const hubname = 'pub-sub';


class TopicHandler {

    constructor(messageHandler, abortHandler) {
        this.messageHandler = messageHandler;
        this.abortHandler = abortHandler;
    }

    onAbortTopic(topic) {
        if (this.abortHandler != null)
            this.abortHandler(topic);
    }

    onNewMessageTopic(topicName, data) {
        if (this.messageHandler != null)
            this.messageHandler(topicName, data);
    }

}

class PubSubConnection {


    constructor(baseUrl, username) {

        this.topicHandlers = [];
        this.newTopicHandler = null;
        this.disConnectedHandler = null;
        this.baseUrl = normalizeUrl(baseUrl);
        this.hubUrl = this.createHubConnectionUrl(this.baseUrl, username);
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(this.hubUrl)
            .withAutomaticReconnect()
            .build();

        this.connection.onclose(err => {
            if (err != null)
                this.disConnectedHandler(err);
            else
                console.log("Connection auto disconnect");
        });

        this.connection.onreconnected(connectionId => {
            console.log("OnReconnect: " + connectionId);
            this.reSubscribe();
        });
    }

    createHubConnectionUrl(baseUrl, username) {
        baseUrl = `${baseUrl}/${hubname}?username=${username}`;
        return baseUrl;
    }

    bindingEvent() {

        this.connection.on("onNewMessage", (topicName, message) => {
            var topicHandler = this.getTopicHandler(topicName);
            if (topicHandler == null)
                return;
            topicHandler.onNewMessageTopic(topicName, message);
        });

        this.connection.on("onAbortTopic", (topic) => {
            var topicHandler = this.topicHandlers[topic.name];
            if (topicHandler == null)
                return;
            topicHandler.onAbortTopic(topic);
        });


        this.connection.on("onNewTopic", (topic) => {
            if (this.newTopicHandler != null) {
                this.newTopicHandler(topic);
            }
        });
    }

    async reSubscribe() {
        if (this.topicHandlers.length == 0)
            return;
        for (var i = 0; i < this.topicHandlers.length; i++) {
            var handler = this.topicHandlers[i];
            await this.connection.invoke("subscribe", handler.key, true);
        }
    }

    getTopicHandler(topicName) {
        if (this.topicHandlers.length == 0)
            return null;
        for (var i = 0; i < this.topicHandlers.length; i++)
            if (this.topicHandlers[i].key == topicName)
                return this.topicHandlers[i].value;
        return null;
    }

    /*---------------public methods -----------------------*/
    
    async connect() {

        await this.connection.start();
        this.bindingEvent();
    }

    async findTopics(topicName) {
        var url = this.baseUrl + "/api/topic/search?name=" + topicName
        return $.ajax({
            url: url,
            datatype: 'json'
        });
    }

    async reConnect() {
        await this.connection.stop();
        await this.connection.start();
    }

    async createTopic(topicName) {
        await this.connection.invoke("create-topic", topicName, {
            IsKeepTopicWhenOwnerDisconnect: true
        });
    }

    async subscribe(topic,
        topicHandler,
        autoCreateTopic = true) {
        await this.connection.invoke("subscribe", topic, autoCreateTopic);
        this.topicHandlers.push({
            key: topic,
            value: topicHandler
        });
    }
    async unSubscribe(topic) {
        await this.connection.invoke("un-subscribe", topic);
        this.topicHandlers = this.topicHandlers.splice(topic, 1)
    }

    async publish(to, message) {
        await this.connection.invoke("publish", to, message);
    }

    async setNewTopicHandler(hanler) {
        this.newTopicHandler = hanler;
    }

    async setDisconnectedHandler(handler) {
        this.disConnectedHandler = handler;
    }
}

function normalizeUrl(baseUrl) {
    if (baseUrl.endsWith('/'))
        baseUrl = baseUrl.slice(0, -1);
    return baseUrl;
}

