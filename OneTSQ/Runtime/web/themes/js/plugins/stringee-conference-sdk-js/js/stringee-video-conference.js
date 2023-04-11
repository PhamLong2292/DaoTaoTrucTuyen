if (function(e, a) {
    var s = e;
    s.version = "0.9.6",
    s.protocol = 1,
    s.transports = [],
    s.j = [],
    s.sockets = {},
    s.connect = function(e, t) {
        var n, r, i = s.util.parseUri(e);
        a && a.location && (i.protocol = i.protocol || a.location.protocol.slice(0, -1),
        i.host = i.host || (a.document ? a.document.domain : a.location.hostname),
        i.port = i.port || a.location.port),
        n = s.util.uniqueUri(i);
        var o = {
            host: i.host,
            secure: "https" == i.protocol,
            port: i.port || ("https" == i.protocol ? 443 : 80),
            query: i.query || ""
        };
        return s.util.merge(o, t),
        !o["force new connection"] && s.sockets[n] || (r = new s.Socket(o)),
        !o["force new connection"] && r && (s.sockets[n] = r),
        (r = r || s.sockets[n]).of(1 < i.path.length ? i.path : "")
    }
}("object" == typeof module ? module.exports : this.io0 = {}, this),
function(e, i) {
    var s = e.util = {}
      , o = /^(?:(?![^:@]+:[^:@\/]*@)([^:\/?#.]+):)?(?:\/\/)?((?:(([^:@]*)(?::([^:@]*))?)?@)?([^:\/?#]*)(?::(\d*))?)(((\/(?:[^?#](?![^?#\/]*\.[^?#\/.]+(?:[?#]|$)))*\/?)?([^?#\/]*))(?:\?([^#]*))?(?:#(.*))?)/
      , a = ["source", "protocol", "authority", "userInfo", "user", "password", "host", "port", "relative", "path", "directory", "file", "query", "anchor"];
    s.parseUri = function(e) {
        for (var t = o.exec(e || ""), n = {}, r = 14; r--; )
            n[a[r]] = t[r] || "";
        return n
    }
    ,
    s.uniqueUri = function(e) {
        var t = e.protocol
          , n = e.host
          , r = e.port;
        return "document"in i ? (n = n || document.domain,
        r = r || ("https" == t && "https:" !== document.location.protocol ? 443 : document.location.port)) : (n = n || "localhost",
        r || "https" != t || (r = 443)),
        (t || "http") + "://" + n + ":" + (r || 80)
    }
    ,
    s.query = function(e, t) {
        var n = s.chunkQuery(e || "")
          , r = [];
        for (var i in s.merge(n, s.chunkQuery(t || "")),
        n)
            n.hasOwnProperty(i) && r.push(i + "=" + n[i]);
        return r.length ? "?" + r.join("&") : ""
    }
    ;
    var t = !(s.chunkQuery = function(e) {
        for (var t, n = {}, r = e.split("&"), i = 0, o = r.length; i < o; ++i)
            (t = r[i].split("="))[0] && (n[t[0]] = t[1]);
        return n
    }
    );
    s.load = function(e) {
        if ("document"in i && "complete" === document.readyState || t)
            return e();
        s.on(i, "load", e, !1)
    }
    ,
    s.on = function(e, t, n, r) {
        e.attachEvent ? e.attachEvent("on" + t, n) : e.addEventListener && e.addEventListener(t, n, r)
    }
    ,
    s.request = function(e) {
        if (e && "undefined" != typeof XDomainRequest)
            return new XDomainRequest;
        if ("undefined" != typeof XMLHttpRequest && (!e || s.ua.hasCORS))
            return new XMLHttpRequest;
        if (!e)
            try {
                return new (window[["Active"].concat("Object").join("X")])("Microsoft.XMLHTTP")
            } catch (e) {}
        return null
    }
    ,
    "undefined" != typeof window && s.load(function() {
        t = !0
    }),
    s.defer = function(e) {
        if (!s.ua.webkit || "undefined" != typeof importScripts)
            return e();
        s.load(function() {
            setTimeout(e, 100)
        })
    }
    ,
    s.merge = function(e, t, n, r) {
        var i, o = r || [], a = void 0 === n ? 2 : n;
        for (i in t)
            t.hasOwnProperty(i) && s.indexOf(o, i) < 0 && ("object" == typeof e[i] && a ? s.merge(e[i], t[i], a - 1, o) : (e[i] = t[i],
            o.push(t[i])));
        return e
    }
    ,
    s.mixin = function(e, t) {
        s.merge(e.prototype, t.prototype)
    }
    ,
    s.inherit = function(e, t) {
        function n() {}
        n.prototype = t.prototype,
        e.prototype = new n
    }
    ,
    s.isArray = Array.isArray || function(e) {
        return "[object Array]" === Object.prototype.toString.call(e)
    }
    ,
    s.intersect = function(e, t) {
        for (var n = [], r = e.length > t.length ? e : t, i = e.length > t.length ? t : e, o = 0, a = i.length; o < a; o++)
            ~s.indexOf(r, i[o]) && n.push(i[o]);
        return n
    }
    ,
    s.indexOf = function(e, t, n) {
        var r = e.length;
        for (n = n < 0 ? n + r < 0 ? 0 : n + r : n || 0; n < r && e[n] !== t; n++)
            ;
        return r <= n ? -1 : n
    }
    ,
    s.toArray = function(e) {
        for (var t = [], n = 0, r = e.length; n < r; n++)
            t.push(e[n]);
        return t
    }
    ,
    s.ua = {},
    s.ua.hasCORS = "undefined" != typeof XMLHttpRequest && function() {
        try {
            var e = new XMLHttpRequest
        } catch (e) {
            return !1
        }
        return null != e.withCredentials
    }(),
    s.ua.webkit = "undefined" != typeof navigator && /webkit/i.test(navigator.userAgent)
}("undefined" != typeof io0 ? io0 : module.exports, this),
function(e, a) {
    function t() {}
    (e.EventEmitter = t).prototype.addListener = t.prototype.on = function(e, t) {
        return this.$events || (this.$events = {}),
        this.$events[e] ? a.util.isArray(this.$events[e]) ? this.$events[e].push(t) : this.$events[e] = [this.$events[e], t] : this.$events[e] = t,
        this
    }
    ,
    t.prototype.once = function(e, t) {
        var n = this;
        function r() {
            n.removeListener(e, r),
            t.apply(this, arguments)
        }
        return r.listener = t,
        this.on(e, r),
        this
    }
    ,
    t.prototype.removeListener = function(e, t) {
        if (this.$events && this.$events[e]) {
            var n = this.$events[e];
            if (a.util.isArray(n)) {
                for (var r = -1, i = 0, o = n.length; i < o; i++)
                    if (n[i] === t || n[i].listener && n[i].listener === t) {
                        r = i;
                        break
                    }
                if (r < 0)
                    return this;
                n.splice(r, 1),
                n.length || delete this.$events[e]
            } else
                (n === t || n.listener && n.listener === t) && delete this.$events[e]
        }
        return this
    }
    ,
    t.prototype.removeAllListeners = function(e) {
        return this.$events && this.$events[e] && (this.$events[e] = null),
        this
    }
    ,
    t.prototype.listeners = function(e) {
        return this.$events || (this.$events = {}),
        this.$events[e] || (this.$events[e] = []),
        a.util.isArray(this.$events[e]) || (this.$events[e] = [this.$events[e]]),
        this.$events[e]
    }
    ,
    t.prototype.emit = function(e) {
        if (!this.$events)
            return !1;
        var t = this.$events[e];
        if (!t)
            return !1;
        var n = Array.prototype.slice.call(arguments, 1);
        if ("function" == typeof t)
            t.apply(this, n);
        else {
            if (!a.util.isArray(t))
                return !1;
            for (var r = t.slice(), i = 0, o = r.length; i < o; i++)
                r[i].apply(this, n)
        }
        return !0
    }
}("undefined" != typeof io0 ? io0 : module.exports, "undefined" != typeof io0 ? io0 : module.parent.exports),
function(exports, nativeJSON) {
    "use strict";
    if (nativeJSON && nativeJSON.parse)
        return exports.JSON = {
            parse: nativeJSON.parse,
            stringify: nativeJSON.stringify
        };
    var JSON = exports.JSON = {};
    function f(e) {
        return e < 10 ? "0" + e : e
    }
    function date(e, t) {
        return isFinite(e.valueOf()) ? e.getUTCFullYear() + "-" + f(e.getUTCMonth() + 1) + "-" + f(e.getUTCDate()) + "T" + f(e.getUTCHours()) + ":" + f(e.getUTCMinutes()) + ":" + f(e.getUTCSeconds()) + "Z" : null
    }
    var cx = /[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g, escapable = /[\\\"\x00-\x1f\x7f-\x9f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g, gap, indent, meta = {
        "\b": "\\b",
        "\t": "\\t",
        "\n": "\\n",
        "\f": "\\f",
        "\r": "\\r",
        '"': '\\"',
        "\\": "\\\\"
    }, rep;
    function quote(e) {
        return escapable.lastIndex = 0,
        escapable.test(e) ? '"' + e.replace(escapable, function(e) {
            var t = meta[e];
            return "string" == typeof t ? t : "\\u" + ("0000" + e.charCodeAt(0).toString(16)).slice(-4)
        }) + '"' : '"' + e + '"'
    }
    function str(e, t) {
        var n, r, i, o, a, s = gap, c = t[e];
        switch (c instanceof Date && (c = date(e)),
        "function" == typeof rep && (c = rep.call(t, e, c)),
        typeof c) {
        case "string":
            return quote(c);
        case "number":
            return isFinite(c) ? String(c) : "null";
        case "boolean":
        case "null":
            return String(c);
        case "object":
            if (!c)
                return "null";
            if (gap += indent,
            a = [],
            "[object Array]" === Object.prototype.toString.apply(c)) {
                for (o = c.length,
                n = 0; n < o; n += 1)
                    a[n] = str(n, c) || "null";
                return i = 0 === a.length ? "[]" : gap ? "[\n" + gap + a.join(",\n" + gap) + "\n" + s + "]" : "[" + a.join(",") + "]",
                gap = s,
                i
            }
            if (rep && "object" == typeof rep)
                for (o = rep.length,
                n = 0; n < o; n += 1)
                    "string" == typeof rep[n] && (i = str(r = rep[n], c)) && a.push(quote(r) + (gap ? ": " : ":") + i);
            else
                for (r in c)
                    Object.prototype.hasOwnProperty.call(c, r) && (i = str(r, c)) && a.push(quote(r) + (gap ? ": " : ":") + i);
            return i = 0 === a.length ? "{}" : gap ? "{\n" + gap + a.join(",\n" + gap) + "\n" + s + "}" : "{" + a.join(",") + "}",
            gap = s,
            i
        }
    }
    JSON.stringify = function(e, t, n) {
        var r;
        if (indent = gap = "",
        "number" == typeof n)
            for (r = 0; r < n; r += 1)
                indent += " ";
        else
            "string" == typeof n && (indent = n);
        if ((rep = t) && "function" != typeof t && ("object" != typeof t || "number" != typeof t.length))
            throw new Error("JSON.stringify");
        return str("", {
            "": e
        })
    }
    ,
    JSON.parse = function(text, reviver) {
        var j;
        function walk(e, t) {
            var n, r, i = e[t];
            if (i && "object" == typeof i)
                for (n in i)
                    Object.prototype.hasOwnProperty.call(i, n) && (void 0 !== (r = walk(i, n)) ? i[n] = r : delete i[n]);
            return reviver.call(e, t, i)
        }
        if (text = String(text),
        cx.lastIndex = 0,
        cx.test(text) && (text = text.replace(cx, function(e) {
            return "\\u" + ("0000" + e.charCodeAt(0).toString(16)).slice(-4)
        })),
        /^[\],:{}\s]*$/.test(text.replace(/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g, "@").replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, "]").replace(/(?:^|:|,)(?:\s*\[)+/g, "")))
            return j = eval("(" + text + ")"),
            "function" == typeof reviver ? walk({
                "": j
            }, "") : j;
        throw new SyntaxError("JSON.parse")
    }
}("undefined" != typeof io0 ? io0 : module.exports, "undefined" != typeof JSON ? JSON : void 0),
function(e, t) {
    var i = e.parser = {}
      , u = i.packets = ["disconnect", "connect", "heartbeat", "message", "json", "event", "ack", "error", "noop"]
      , p = i.reasons = ["transport not supported", "client not handshaken", "unauthorized"]
      , l = i.advice = ["reconnect"]
      , f = t.JSON
      , m = t.util.indexOf;
    i.encodePacket = function(e) {
        var t = m(u, e.type)
          , n = e.id || ""
          , r = e.endpoint || ""
          , i = e.ack
          , o = null;
        switch (e.type) {
        case "error":
            var a = e.reason ? m(p, e.reason) : ""
              , s = e.advice ? m(l, e.advice) : "";
            "" === a && "" === s || (o = a + ("" !== s ? "+" + s : ""));
            break;
        case "message":
            "" !== e.data && (o = e.data);
            break;
        case "event":
            var c = {
                name: e.name
            };
            e.args && e.args.length && (c.args = e.args),
            o = f.stringify(c);
            break;
        case "json":
            o = f.stringify(e.data);
            break;
        case "connect":
            e.qs && (o = e.qs);
            break;
        case "ack":
            o = e.ackId + (e.args && e.args.length ? "+" + f.stringify(e.args) : "")
        }
        var d = [t, n + ("data" == i ? "+" : ""), r];
        return null != o && d.push(o),
        d.join(":")
    }
    ,
    i.encodePayload = function(e) {
        var t = "";
        if (1 == e.length)
            return e[0];
        for (var n = 0, r = e.length; n < r; n++) {
            t += "�" + e[n].length + "�" + e[n]
        }
        return t
    }
    ;
    var o = /([^:]+):([0-9]+)?(\+)?:([^:]+)?:?([\s\S]*)?/;
    i.decodePacket = function(e) {
        if (!(r = e.match(o)))
            return {};
        var t = r[2] || ""
          , n = (e = r[5] || "",
        {
            type: u[r[1]],
            endpoint: r[4] || ""
        });
        switch (t && (n.id = t,
        r[3] ? n.ack = "data" : n.ack = !0),
        n.type) {
        case "error":
            var r = e.split("+");
            n.reason = p[r[0]] || "",
            n.advice = l[r[1]] || "";
            break;
        case "message":
            n.data = e || "";
            break;
        case "event":
            try {
                var i = f.parse(e);
                n.name = i.name,
                n.args = i.args
            } catch (e) {}
            n.args = n.args || [];
            break;
        case "json":
            try {
                n.data = f.parse(e)
            } catch (e) {}
            break;
        case "connect":
            n.qs = e || "";
            break;
        case "ack":
            if ((r = e.match(/^([0-9]+)(\+)?(.*)/)) && (n.ackId = r[1],
            n.args = [],
            r[3]))
                try {
                    n.args = r[3] ? f.parse(r[3]) : []
                } catch (e) {}
        }
        return n
    }
    ,
    i.decodePayload = function(e) {
        if ("�" == e.charAt(0)) {
            for (var t = [], n = 1, r = ""; n < e.length; n++)
                "�" == e.charAt(n) ? (t.push(i.decodePacket(e.substr(n + 1).substr(0, r))),
                n += Number(r) + 1,
                r = "") : r += e.charAt(n);
            return t
        }
        return [i.decodePacket(e)]
    }
}("undefined" != typeof io0 ? io0 : module.exports, "undefined" != typeof io0 ? io0 : module.parent.exports),
function(e, i) {
    function t(e, t) {
        this.socket = e,
        this.sessid = t
    }
    e.Transport = t,
    i.util.mixin(t, i.EventEmitter),
    t.prototype.onData = function(e) {
        if (this.clearCloseTimeout(),
        (this.socket.connected || this.socket.connecting || this.socket.reconnecting) && this.setCloseTimeout(),
        "" !== e) {
            var t = i.parser.decodePayload(e);
            if (t && t.length)
                for (var n = 0, r = t.length; n < r; n++)
                    this.onPacket(t[n])
        }
        return this
    }
    ,
    t.prototype.onPacket = function(e) {
        return this.socket.setHeartbeatTimeout(),
        "heartbeat" == e.type ? this.onHeartbeat() : ("connect" == e.type && "" == e.endpoint && this.onConnect(),
        "error" == e.type && "reconnect" == e.advice && (this.open = !1),
        this.socket.onPacket(e),
        this)
    }
    ,
    t.prototype.setCloseTimeout = function() {
        if (!this.closeTimeout) {
            var e = this;
            this.closeTimeout = setTimeout(function() {
                e.onDisconnect()
            }, this.socket.closeTimeout)
        }
    }
    ,
    t.prototype.onDisconnect = function() {
        return this.close && this.open && this.close(),
        this.clearTimeouts(),
        this.socket.onDisconnect(),
        this
    }
    ,
    t.prototype.onConnect = function() {
        return this.socket.onConnect(),
        this
    }
    ,
    t.prototype.clearCloseTimeout = function() {
        this.closeTimeout && (clearTimeout(this.closeTimeout),
        this.closeTimeout = null)
    }
    ,
    t.prototype.clearTimeouts = function() {
        this.clearCloseTimeout(),
        this.reopenTimeout && clearTimeout(this.reopenTimeout)
    }
    ,
    t.prototype.packet = function(e) {
        this.send(i.parser.encodePacket(e))
    }
    ,
    t.prototype.onHeartbeat = function(e) {
        this.packet({
            type: "heartbeat"
        })
    }
    ,
    t.prototype.onOpen = function() {
        this.open = !0,
        this.clearCloseTimeout(),
        this.socket.onOpen()
    }
    ,
    t.prototype.onClose = function() {
        this.open = !1,
        this.socket.onClose(),
        this.onDisconnect()
    }
    ,
    t.prototype.prepareUrl = function() {
        var e = this.socket.options;
        return this.scheme() + "://" + e.host + ":" + e.port + "/" + e.resource + "/" + i.protocol + "/" + this.name + "/" + this.sessid
    }
    ,
    t.prototype.ready = function(e, t) {
        t.call(this)
    }
}("undefined" != typeof io0 ? io0 : module.exports, "undefined" != typeof io0 ? io0 : module.parent.exports),
function(e, c, n) {
    function t(e) {
        if (this.options = {
            port: 80,
            secure: !1,
            document: "document"in n && document,
            resource: "socket.io",
            transports: c.transports,
            "connect timeout": 1e4,
            "try multiple transports": !0,
            reconnect: !0,
            "reconnection delay": 500,
            "reconnection limit": 1 / 0,
            "reopen delay": 3e3,
            "max reconnection attempts": 10,
            "sync disconnect on unload": !0,
            "auto connect": !0,
            "flash policy port": 10843
        },
        c.util.merge(this.options, e),
        this.connected = !1,
        this.open = !1,
        this.connecting = !1,
        this.reconnecting = !1,
        this.namespaces = {},
        this.buffer = [],
        this.doBuffer = !1,
        this.options["sync disconnect on unload"] && (!this.isXDomain() || c.util.ua.hasCORS)) {
            var t = this;
            c.util.on(n, "unload", function() {
                t.disconnectSync()
            }, !1)
        }
        this.options["auto connect"] && this.connect()
    }
    function d() {}
    e.Socket = t,
    c.util.mixin(t, c.EventEmitter),
    t.prototype.of = function(e) {
        return this.namespaces[e] || (this.namespaces[e] = new c.SocketNamespace(this,e),
        "" !== e && this.namespaces[e].packet({
            type: "connect"
        })),
        this.namespaces[e]
    }
    ,
    t.prototype.publish = function() {
        var e;
        for (var t in this.emit.apply(this, arguments),
        this.namespaces)
            this.namespaces.hasOwnProperty(t) && (e = this.of(t)).$emit.apply(e, arguments)
    }
    ,
    t.prototype.handshake = function(t) {
        var n = this
          , e = this.options;
        function r(e) {
            e instanceof Error ? n.onError(e.message) : t.apply(null, e.split(":"))
        }
        var i = ["http" + (e.secure ? "s" : "") + ":/", e.host + ":" + e.port, e.resource, c.protocol, c.util.query(this.options.query, "t=" + +new Date)].join("/");
        if (this.isXDomain() && !c.util.ua.hasCORS) {
            var o = document.getElementsByTagName("script")[0]
              , a = document.createElement("script");
            a.src = i + "&jsonp=" + c.j.length,
            o.parentNode.insertBefore(a, o),
            c.j.push(function(e) {
                r(e),
                a.parentNode.removeChild(a)
            })
        } else {
            var s = c.util.request();
            s.open("GET", i, !0),
            s.withCredentials = !0,
            s.onreadystatechange = function() {
                4 == s.readyState && (s.onreadystatechange = d,
                200 == s.status ? r(s.responseText) : !n.reconnecting && n.onError(s.responseText))
            }
            ,
            s.send(null)
        }
    }
    ,
    t.prototype.getTransport = function(e) {
        for (var t, n = e || this.transports, r = 0; t = n[r]; r++)
            if (c.Transport[t] && c.Transport[t].check(this) && (!this.isXDomain() || c.Transport[t].xdomainCheck()))
                return new c.Transport[t](this,this.sessionid);
        return null
    }
    ,
    t.prototype.connect = function(i) {
        if (this.connecting)
            return this;
        var o = this;
        return this.handshake(function(e, t, n, r) {
            o.sessionid = e,
            o.closeTimeout = 1e3 * n,
            o.heartbeatTimeout = 1e3 * t,
            o.transports = r ? c.util.intersect(r.split(","), o.options.transports) : o.options.transports,
            o.setHeartbeatTimeout(),
            function t(e) {
                if (o.transport && o.transport.clearTimeouts(),
                o.transport = o.getTransport(e),
                !o.transport)
                    return o.publish("connect_failed");
                o.transport.ready(o, function() {
                    o.connecting = !0,
                    o.publish("connecting", o.transport.name),
                    o.transport.open(),
                    o.options["connect timeout"] && (o.connectTimeoutTimer = setTimeout(function() {
                        if (!o.connected && (o.connecting = !1,
                        o.options["try multiple transports"])) {
                            o.remainingTransports || (o.remainingTransports = o.transports.slice(0));
                            for (var e = o.remainingTransports; 0 < e.length && e.splice(0, 1)[0] != o.transport.name; )
                                ;
                            e.length ? t(e) : o.publish("connect_failed")
                        }
                    }, o.options["connect timeout"]))
                })
            }(o.transports),
            o.once("connect", function() {
                clearTimeout(o.connectTimeoutTimer),
                i && "function" == typeof i && i()
            })
        }),
        this
    }
    ,
    t.prototype.setHeartbeatTimeout = function() {
        clearTimeout(this.heartbeatTimeoutTimer);
        var e = this;
        this.heartbeatTimeoutTimer = setTimeout(function() {
            e.transport.onClose()
        }, this.heartbeatTimeout)
    }
    ,
    t.prototype.packet = function(e) {
        return this.connected && !this.doBuffer ? this.transport.packet(e) : this.buffer.push(e),
        this
    }
    ,
    t.prototype.setBuffer = function(e) {
        !(this.doBuffer = e) && this.connected && this.buffer.length && (this.transport.payload(this.buffer),
        this.buffer = [])
    }
    ,
    t.prototype.disconnect = function() {
        return (this.connected || this.connecting) && (this.open && this.of("").packet({
            type: "disconnect"
        }),
        this.onDisconnect("booted")),
        this
    }
    ,
    t.prototype.disconnectSync = function() {
        var e = c.util.request()
          , t = this.resource + "/" + c.protocol + "/" + this.sessionid;
        e.open("GET", t, !0),
        this.onDisconnect("booted")
    }
    ,
    t.prototype.isXDomain = function() {
        var e = n.location.port || ("https:" == n.location.protocol ? 443 : 80);
        return this.options.host !== n.location.hostname || this.options.port != e
    }
    ,
    t.prototype.onConnect = function() {
        this.connected || (this.connected = !0,
        this.connecting = !1,
        this.doBuffer || this.setBuffer(!1),
        this.emit("connect"))
    }
    ,
    t.prototype.onOpen = function() {
        this.open = !0
    }
    ,
    t.prototype.onClose = function() {
        this.open = !1,
        clearTimeout(this.heartbeatTimeoutTimer)
    }
    ,
    t.prototype.onPacket = function(e) {
        this.of(e.endpoint).onPacket(e)
    }
    ,
    t.prototype.onError = function(e) {
        e && e.advice && "reconnect" === e.advice && (this.connected || this.connecting) && (this.disconnect(),
        this.options.reconnect && this.reconnect()),
        this.publish("error", e && e.reason ? e.reason : e)
    }
    ,
    t.prototype.onDisconnect = function(e) {
        var t = this.connected
          , n = this.connecting;
        this.connected = !1,
        this.connecting = !1,
        this.open = !1,
        (t || n) && (this.transport.close(),
        this.transport.clearTimeouts(),
        t && (this.publish("disconnect", e),
        "booted" != e && this.options.reconnect && !this.reconnecting && this.reconnect()))
    }
    ,
    t.prototype.reconnect = function() {
        this.reconnecting = !0,
        this.reconnectionAttempts = 0,
        this.reconnectionDelay = this.options["reconnection delay"];
        var t = this
          , e = this.options["max reconnection attempts"]
          , n = this.options["try multiple transports"]
          , r = this.options["reconnection limit"];
        function i() {
            if (t.connected) {
                for (var e in t.namespaces)
                    t.namespaces.hasOwnProperty(e) && "" !== e && t.namespaces[e].packet({
                        type: "connect"
                    });
                t.publish("reconnect", t.transport.name, t.reconnectionAttempts)
            }
            clearTimeout(t.reconnectionTimer),
            t.removeListener("connect_failed", o),
            t.removeListener("connect", o),
            t.reconnecting = !1,
            delete t.reconnectionAttempts,
            delete t.reconnectionDelay,
            delete t.reconnectionTimer,
            delete t.redoTransports,
            t.options["try multiple transports"] = n
        }
        function o() {
            if (t.reconnecting)
                return t.connected ? i() : t.connecting && t.reconnecting ? t.reconnectionTimer = setTimeout(o, 1e3) : void (t.reconnectionAttempts++ >= e ? t.redoTransports ? (t.publish("reconnect_failed"),
                i()) : (t.on("connect_failed", o),
                t.options["try multiple transports"] = !0,
                t.transport = t.getTransport(),
                t.redoTransports = !0,
                t.connect()) : (t.reconnectionDelay < r && (t.reconnectionDelay *= 2),
                t.connect(),
                t.publish("reconnecting", t.reconnectionDelay, t.reconnectionAttempts),
                t.reconnectionTimer = setTimeout(o, t.reconnectionDelay)))
        }
        this.options["try multiple transports"] = !1,
        this.reconnectionTimer = setTimeout(o, this.reconnectionDelay),
        this.on("connect", o)
    }
}("undefined" != typeof io0 ? io0 : module.exports, "undefined" != typeof io0 ? io0 : module.parent.exports, this),
function(e, i) {
    function t(e, t) {
        this.socket = e,
        this.name = t || "",
        this.flags = {},
        this.json = new n(this,"json"),
        this.ackPackets = 0,
        this.acks = {}
    }
    function n(e, t) {
        this.namespace = e,
        this.name = t
    }
    e.SocketNamespace = t,
    i.util.mixin(t, i.EventEmitter),
    t.prototype.$emit = i.EventEmitter.prototype.emit,
    t.prototype.of = function() {
        return this.socket.of.apply(this.socket, arguments)
    }
    ,
    t.prototype.packet = function(e) {
        return e.endpoint = this.name,
        this.socket.packet(e),
        this.flags = {},
        this
    }
    ,
    t.prototype.send = function(e, t) {
        var n = {
            type: this.flags.json ? "json" : "message",
            data: e
        };
        return "function" == typeof t && (n.id = ++this.ackPackets,
        n.ack = !0,
        this.acks[n.id] = t),
        this.packet(n)
    }
    ,
    t.prototype.emit = function(e) {
        var t = Array.prototype.slice.call(arguments, 1)
          , n = t[t.length - 1]
          , r = {
            type: "event",
            name: e
        };
        return "function" == typeof n && (r.id = ++this.ackPackets,
        r.ack = "data",
        this.acks[r.id] = n,
        t = t.slice(0, t.length - 1)),
        r.args = t,
        this.packet(r)
    }
    ,
    t.prototype.disconnect = function() {
        return "" === this.name ? this.socket.disconnect() : (this.packet({
            type: "disconnect"
        }),
        this.$emit("disconnect")),
        this
    }
    ,
    t.prototype.onPacket = function(e) {
        var t = this;
        function n() {
            t.packet({
                type: "ack",
                args: i.util.toArray(arguments),
                ackId: e.id
            })
        }
        switch (e.type) {
        case "connect":
            this.$emit("connect");
            break;
        case "disconnect":
            "" === this.name ? this.socket.onDisconnect(e.reason || "booted") : this.$emit("disconnect", e.reason);
            break;
        case "message":
        case "json":
            var r = ["message", e.data];
            "data" == e.ack ? r.push(n) : e.ack && this.packet({
                type: "ack",
                ackId: e.id
            }),
            this.$emit.apply(this, r);
            break;
        case "event":
            r = [e.name].concat(e.args);
            "data" == e.ack && r.push(n),
            this.$emit.apply(this, r);
            break;
        case "ack":
            this.acks[e.ackId] && (this.acks[e.ackId].apply(this, e.args),
            delete this.acks[e.ackId]);
            break;
        case "error":
            e.advice ? this.socket.onError(e) : "unauthorized" == e.reason ? this.$emit("connect_failed", e.reason) : this.$emit("error", e.reason)
        }
    }
    ,
    n.prototype.send = function() {
        this.namespace.flags[this.name] = !0,
        this.namespace.send.apply(this.namespace, arguments)
    }
    ,
    n.prototype.emit = function() {
        this.namespace.flags[this.name] = !0,
        this.namespace.emit.apply(this.namespace, arguments)
    }
}("undefined" != typeof io0 ? io0 : module.exports, "undefined" != typeof io0 ? io0 : module.parent.exports),
function(e, r, i) {
    function t(e) {
        r.Transport.apply(this, arguments)
    }
    e.websocket = t,
    r.util.inherit(t, r.Transport),
    t.prototype.name = "websocket",
    t.prototype.open = function() {
        var e, t = r.util.query(this.socket.options.query), n = this;
        return e || (e = i.MozWebSocket || i.WebSocket),
        this.websocket = new e(this.prepareUrl() + t),
        this.websocket.onopen = function() {
            n.onOpen(),
            n.socket.setBuffer(!1)
        }
        ,
        this.websocket.onmessage = function(e) {
            n.onData(e.data)
        }
        ,
        this.websocket.onclose = function() {
            n.onClose(),
            n.socket.setBuffer(!0)
        }
        ,
        this.websocket.onerror = function(e) {
            n.onError(e)
        }
        ,
        this
    }
    ,
    t.prototype.send = function(e) {
        return this.websocket.send(e),
        this
    }
    ,
    t.prototype.payload = function(e) {
        for (var t = 0, n = e.length; t < n; t++)
            this.packet(e[t]);
        return this
    }
    ,
    t.prototype.close = function() {
        return this.websocket.close(),
        this
    }
    ,
    t.prototype.onError = function(e) {
        this.socket.onError(e)
    }
    ,
    t.prototype.scheme = function() {
        return this.socket.options.secure ? "wss" : "ws"
    }
    ,
    t.check = function() {
        return "WebSocket"in i && !("__addTask"in WebSocket) || "MozWebSocket"in i
    }
    ,
    t.xdomainCheck = function() {
        return !0
    }
    ,
    r.transports.push("websocket")
}("undefined" != typeof io0 ? io0.Transport : module.exports, "undefined" != typeof io0 ? io0 : module.parent.exports, this),
function(e, n) {
    function a() {
        n.Transport.websocket.apply(this, arguments)
    }
    e.flashsocket = a,
    n.util.inherit(a, n.Transport.websocket),
    a.prototype.name = "flashsocket",
    a.prototype.open = function() {
        var e = this
          , t = arguments;
        return WebSocket.__addTask(function() {
            n.Transport.websocket.prototype.open.apply(e, t)
        }),
        this
    }
    ,
    a.prototype.send = function() {
        var e = this
          , t = arguments;
        return WebSocket.__addTask(function() {
            n.Transport.websocket.prototype.send.apply(e, t)
        }),
        this
    }
    ,
    a.prototype.close = function() {
        return WebSocket.__tasks.length = 0,
        n.Transport.websocket.prototype.close.call(this),
        this
    }
    ,
    a.prototype.ready = function(r, i) {
        function e() {
            var e = r.options
              , t = e["flash policy port"]
              , n = ["http" + (e.secure ? "s" : "") + ":/", e.host + ":" + e.port, e.resource, "static/flashsocket", "WebSocketMain" + (r.isXDomain() ? "Insecure" : "") + ".swf"];
            a.loaded || ("undefined" == typeof WEB_SOCKET_SWF_LOCATION && (WEB_SOCKET_SWF_LOCATION = n.join("/")),
            843 !== t && WebSocket.loadFlashPolicyFile("xmlsocket://" + e.host + ":" + t),
            WebSocket.__initialize(),
            a.loaded = !0),
            i.call(o)
        }
        var o = this;
        if (document.body)
            return e();
        n.util.load(e)
    }
    ,
    a.check = function() {
        return !!("undefined" != typeof WebSocket && "__initialize"in WebSocket && swfobject) && 10 <= swfobject.getFlashPlayerVersion().major
    }
    ,
    a.xdomainCheck = function() {
        return !0
    }
    ,
    "undefined" != typeof window && (WEB_SOCKET_DISABLE_AUTO_INITIALIZATION = !0),
    n.transports.push("flashsocket")
}("undefined" != typeof io0 ? io0.Transport : module.exports, "undefined" != typeof io0 ? io0 : module.parent.exports),
"undefined" != typeof window)
    var swfobject = function() {
        var c, d, u, p, s, l, v = "undefined", y = "object", f = "Shockwave Flash", m = "application/x-shockwave-flash", h = "SWFObjectExprInst", e = "onreadystatechange", g = window, S = document, b = navigator, C = !1, r = [function() {
            C ? function() {
                var t = S.getElementsByTagName("body")[0]
                  , n = N(y);
                n.setAttribute("type", m);
                var r = t.appendChild(n);
                if (r) {
                    var i = 0;
                    !function() {
                        if (typeof r.GetVariable != v) {
                            var e = r.GetVariable("$version");
                            e && (e = e.split(" ")[1].split(","),
                            w.pv = [parseInt(e[0], 10), parseInt(e[1], 10), parseInt(e[2], 10)])
                        } else if (i < 10)
                            return i++,
                            setTimeout(arguments.callee, 10);
                        t.removeChild(n),
                        r = null,
                        R()
                    }()
                } else
                    R()
            }() : R()
        }
        ], E = [], k = [], a = [], i = !1, T = !1, o = !0, w = function() {
            var e = typeof S.getElementById != v && typeof S.getElementsByTagName != v && typeof S.createElement != v
              , t = b.userAgent.toLowerCase()
              , n = b.platform.toLowerCase()
              , r = /win/.test(n || t)
              , i = /mac/.test(n || t)
              , o = !!/webkit/.test(t) && parseFloat(t.replace(/^.*webkit\/(\d+(\.\d+)?).*$/, "$1"))
              , a = !1
              , s = [0, 0, 0]
              , c = null;
            if (typeof b.plugins != v && typeof b.plugins[f] == y)
                !(c = b.plugins[f].description) || typeof b.mimeTypes != v && b.mimeTypes[m] && !b.mimeTypes[m].enabledPlugin || (a = !(C = !0),
                c = c.replace(/^.*\s+(\S+\s+\S+$)/, "$1"),
                s[0] = parseInt(c.replace(/^(.*)\..*$/, "$1"), 10),
                s[1] = parseInt(c.replace(/^.*\.(.*)\s.*$/, "$1"), 10),
                s[2] = /[a-zA-Z]/.test(c) ? parseInt(c.replace(/^.*[a-zA-Z]+(.*)$/, "$1"), 10) : 0);
            else if (typeof g[["Active"].concat("Object").join("X")] != v)
                try {
                    var d = new (window[["Active"].concat("Object").join("X")])("ShockwaveFlash.ShockwaveFlash");
                    d && (c = d.GetVariable("$version")) && (a = !0,
                    c = c.split(" ")[1].split(","),
                    s = [parseInt(c[0], 10), parseInt(c[1], 10), parseInt(c[2], 10)])
                } catch (e) {}
            return {
                w3: e,
                pv: s,
                wk: o,
                ie: a,
                win: r,
                mac: i
            }
        }();
        w.w3 && ((typeof S.readyState != v && "complete" == S.readyState || typeof S.readyState == v && (S.getElementsByTagName("body")[0] || S.body)) && t(),
        i || (typeof S.addEventListener != v && S.addEventListener("DOMContentLoaded", t, !1),
        w.ie && w.win && (S.attachEvent(e, function() {
            "complete" == S.readyState && (S.detachEvent(e, arguments.callee),
            t())
        }),
        g == top && function() {
            if (!i) {
                try {
                    S.documentElement.doScroll("left")
                } catch (e) {
                    return setTimeout(arguments.callee, 0)
                }
                t()
            }
        }()),
        w.wk && function() {
            i || (/loaded|complete/.test(S.readyState) ? t() : setTimeout(arguments.callee, 0))
        }(),
        L(t)));
        function t() {
            if (!i) {
                try {
                    var e = S.getElementsByTagName("body")[0].appendChild(N("span"));
                    e.parentNode.removeChild(e)
                } catch (e) {
                    return
                }
                i = !0;
                for (var t = r.length, n = 0; n < t; n++)
                    r[n]()
            }
        }
        function n(e) {
            i ? e() : r[r.length] = e
        }
        function L(e) {
            if (typeof g.addEventListener != v)
                g.addEventListener("load", e, !1);
            else if (typeof S.addEventListener != v)
                S.addEventListener("load", e, !1);
            else if (typeof g.attachEvent != v)
                r = "onload",
                i = e,
                (n = g).attachEvent(r, i),
                a[a.length] = [n, r, i];
            else if ("function" == typeof g.onload) {
                var t = g.onload;
                g.onload = function() {
                    t(),
                    e()
                }
            } else
                g.onload = e;
            var n, r, i
        }
        function R() {
            var e = E.length;
            if (0 < e)
                for (var t = 0; t < e; t++) {
                    var n = E[t].id
                      , r = E[t].callbackFn
                      , i = {
                        success: !1,
                        id: n
                    };
                    if (0 < w.pv[0]) {
                        var o = M(n);
                        if (o)
                            if (!F(E[t].swfVersion) || w.wk && w.wk < 312)
                                if (E[t].expressInstall && P()) {
                                    var a = {};
                                    a.data = E[t].expressInstall,
                                    a.width = o.getAttribute("width") || "0",
                                    a.height = o.getAttribute("height") || "0",
                                    o.getAttribute("class") && (a.styleclass = o.getAttribute("class")),
                                    o.getAttribute("align") && (a.align = o.getAttribute("align"));
                                    for (var s = {}, c = o.getElementsByTagName("param"), d = c.length, u = 0; u < d; u++)
                                        "movie" != c[u].getAttribute("name").toLowerCase() && (s[c[u].getAttribute("name")] = c[u].getAttribute("value"));
                                    I(a, s, n, r)
                                } else
                                    x(o),
                                    r && r(i);
                            else
                                B(n, !0),
                                r && (i.success = !0,
                                i.ref = _(n),
                                r(i))
                    } else if (B(n, !0),
                    r) {
                        var p = _(n);
                        p && typeof p.SetVariable != v && (i.success = !0,
                        i.ref = p),
                        r(i)
                    }
                }
        }
        function _(e) {
            var t = null
              , n = M(e);
            if (n && "OBJECT" == n.nodeName)
                if (typeof n.SetVariable != v)
                    t = n;
                else {
                    var r = n.getElementsByTagName(y)[0];
                    r && (t = r)
                }
            return t
        }
        function P() {
            return !T && F("6.0.65") && (w.win || w.mac) && !(w.wk && w.wk < 312)
        }
        function I(e, t, n, r) {
            u = r || null,
            p = {
                success: !(T = !0),
                id: n
            };
            var i = M(n);
            if (i) {
                "OBJECT" == i.nodeName ? (c = O(i),
                d = null) : (c = i,
                d = n),
                e.id = h,
                (typeof e.width == v || !/%$/.test(e.width) && parseInt(e.width, 10) < 310) && (e.width = "310"),
                (typeof e.height == v || !/%$/.test(e.height) && parseInt(e.height, 10) < 137) && (e.height = "137"),
                S.title = S.title.slice(0, 47) + " - Flash Player Installation";
                var o = w.ie && w.win ? ["Active"].concat("").join("X") : "PlugIn"
                  , a = "MMredirectURL=" + g.location.toString().replace(/&/g, "%26") + "&MMplayerType=" + o + "&MMdoctitle=" + S.title;
                if (typeof t.flashvars != v ? t.flashvars += "&" + a : t.flashvars = a,
                w.ie && w.win && 4 != i.readyState) {
                    var s = N("div");
                    n += "SWFObjectNew",
                    s.setAttribute("id", n),
                    i.parentNode.insertBefore(s, i),
                    i.style.display = "none",
                    function() {
                        4 == i.readyState ? i.parentNode.removeChild(i) : setTimeout(arguments.callee, 10)
                    }()
                }
                A(e, t, n)
            }
        }
        function x(e) {
            if (w.ie && w.win && 4 != e.readyState) {
                var t = N("div");
                e.parentNode.insertBefore(t, e),
                t.parentNode.replaceChild(O(e), t),
                e.style.display = "none",
                function() {
                    4 == e.readyState ? e.parentNode.removeChild(e) : setTimeout(arguments.callee, 10)
                }()
            } else
                e.parentNode.replaceChild(O(e), e)
        }
        function O(e) {
            var t = N("div");
            if (w.win && w.ie)
                t.innerHTML = e.innerHTML;
            else {
                var n = e.getElementsByTagName(y)[0];
                if (n) {
                    var r = n.childNodes;
                    if (r)
                        for (var i = r.length, o = 0; o < i; o++)
                            1 == r[o].nodeType && "PARAM" == r[o].nodeName || 8 == r[o].nodeType || t.appendChild(r[o].cloneNode(!0))
                }
            }
            return t
        }
        function A(e, t, n) {
            var r, i = M(n);
            if (w.wk && w.wk < 312)
                return r;
            if (i)
                if (typeof e.id == v && (e.id = n),
                w.ie && w.win) {
                    var o = "";
                    for (var a in e)
                        e[a] != Object.prototype[a] && ("data" == a.toLowerCase() ? t.movie = e[a] : "styleclass" == a.toLowerCase() ? o += ' class="' + e[a] + '"' : "classid" != a.toLowerCase() && (o += " " + a + '="' + e[a] + '"'));
                    var s = "";
                    for (var c in t)
                        t[c] != Object.prototype[c] && (s += '<param name="' + c + '" value="' + t[c] + '" />');
                    i.outerHTML = '<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"' + o + ">" + s + "</object>",
                    k[k.length] = e.id,
                    r = M(e.id)
                } else {
                    var d = N(y);
                    for (var u in d.setAttribute("type", m),
                    e)
                        e[u] != Object.prototype[u] && ("styleclass" == u.toLowerCase() ? d.setAttribute("class", e[u]) : "classid" != u.toLowerCase() && d.setAttribute(u, e[u]));
                    for (var p in t)
                        t[p] != Object.prototype[p] && "movie" != p.toLowerCase() && D(d, p, t[p]);
                    i.parentNode.replaceChild(d, i),
                    r = d
                }
            return r
        }
        function D(e, t, n) {
            var r = N("param");
            r.setAttribute("name", t),
            r.setAttribute("value", n),
            e.appendChild(r)
        }
        function V(e) {
            var t = M(e);
            t && "OBJECT" == t.nodeName && (w.ie && w.win ? (t.style.display = "none",
            function() {
                4 == t.readyState ? function(e) {
                    var t = M(e);
                    if (t) {
                        for (var n in t)
                            "function" == typeof t[n] && (t[n] = null);
                        t.parentNode.removeChild(t)
                    }
                }(e) : setTimeout(arguments.callee, 10)
            }()) : t.parentNode.removeChild(t))
        }
        function M(e) {
            var t = null;
            try {
                t = S.getElementById(e)
            } catch (e) {}
            return t
        }
        function N(e) {
            return S.createElement(e)
        }
        function F(e) {
            var t = w.pv
              , n = e.split(".");
            return n[0] = parseInt(n[0], 10),
            n[1] = parseInt(n[1], 10) || 0,
            n[2] = parseInt(n[2], 10) || 0,
            t[0] > n[0] || t[0] == n[0] && t[1] > n[1] || t[0] == n[0] && t[1] == n[1] && t[2] >= n[2]
        }
        function W(e, t, n, r) {
            if (!w.ie || !w.mac) {
                var i = S.getElementsByTagName("head")[0];
                if (i) {
                    var o = n && "string" == typeof n ? n : "screen";
                    if (r && (l = s = null),
                    !s || l != o) {
                        var a = N("style");
                        a.setAttribute("type", "text/css"),
                        a.setAttribute("media", o),
                        s = i.appendChild(a),
                        w.ie && w.win && typeof S.styleSheets != v && 0 < S.styleSheets.length && (s = S.styleSheets[S.styleSheets.length - 1]),
                        l = o
                    }
                    w.ie && w.win ? s && typeof s.addRule == y && s.addRule(e, t) : s && typeof S.createTextNode != v && s.appendChild(S.createTextNode(e + " {" + t + "}"))
                }
            }
        }
        function B(e, t) {
            if (o) {
                var n = t ? "visible" : "hidden";
                i && M(e) ? M(e).style.visibility = n : W("#" + e, "visibility:" + n)
            }
        }
        function j(e) {
            return null != /[\\\"<>\.;]/.exec(e) && typeof encodeURIComponent != v ? encodeURIComponent(e) : e
        }
        w.ie && w.win && window.attachEvent("onunload", function() {
            for (var e = a.length, t = 0; t < e; t++)
                a[t][0].detachEvent(a[t][1], a[t][2]);
            for (var n = k.length, r = 0; r < n; r++)
                V(k[r]);
            for (var i in w)
                w[i] = null;
            for (var o in w = null,
            swfobject)
                swfobject[o] = null;
            swfobject = null
        });
        return {
            registerObject: function(e, t, n, r) {
                if (w.w3 && e && t) {
                    var i = {};
                    i.id = e,
                    i.swfVersion = t,
                    i.expressInstall = n,
                    i.callbackFn = r,
                    E[E.length] = i,
                    B(e, !1)
                } else
                    r && r({
                        success: !1,
                        id: e
                    })
            },
            getObjectById: function(e) {
                if (w.w3)
                    return _(e)
            },
            embedSWF: function(a, s, c, d, u, p, l, f, m, h) {
                var g = {
                    success: !1,
                    id: s
                };
                w.w3 && !(w.wk && w.wk < 312) && a && s && c && d && u ? (B(s, !1),
                n(function() {
                    c += "",
                    d += "";
                    var e = {};
                    if (m && typeof m === y)
                        for (var t in m)
                            e[t] = m[t];
                    e.data = a,
                    e.width = c,
                    e.height = d;
                    var n = {};
                    if (f && typeof f === y)
                        for (var r in f)
                            n[r] = f[r];
                    if (l && typeof l === y)
                        for (var i in l)
                            typeof n.flashvars != v ? n.flashvars += "&" + i + "=" + l[i] : n.flashvars = i + "=" + l[i];
                    if (F(u)) {
                        var o = A(e, n, s);
                        e.id == s && B(s, !0),
                        g.success = !0,
                        g.ref = o
                    } else {
                        if (p && P())
                            return e.data = p,
                            void I(e, n, s, h);
                        B(s, !0)
                    }
                    h && h(g)
                })) : h && h(g)
            },
            switchOffAutoHideShow: function() {
                o = !1
            },
            ua: w,
            getFlashPlayerVersion: function() {
                return {
                    major: w.pv[0],
                    minor: w.pv[1],
                    release: w.pv[2]
                }
            },
            hasFlashPlayerVersion: F,
            createSWF: function(e, t, n) {
                return w.w3 ? A(e, t, n) : void 0
            },
            showExpressInstall: function(e, t, n, r) {
                w.w3 && P() && I(e, t, n, r)
            },
            removeSWF: function(e) {
                w.w3 && V(e)
            },
            createCSS: function(e, t, n, r) {
                w.w3 && W(e, t, n, r)
            },
            addDomLoadEvent: n,
            addLoadEvent: L,
            getQueryParamValue: function(e) {
                var t = S.location.search || S.location.hash;
                if (t) {
                    if (/\?/.test(t) && (t = t.split("?")[1]),
                    null == e)
                        return j(t);
                    for (var n = t.split("&"), r = 0; r < n.length; r++)
                        if (n[r].substring(0, n[r].indexOf("=")) == e)
                            return j(n[r].substring(n[r].indexOf("=") + 1))
                }
                return ""
            },
            expressInstallCallback: function() {
                if (T) {
                    var e = M(h);
                    e && c && (e.parentNode.replaceChild(c, e),
                    d && (B(d, !0),
                    w.ie && w.win && (c.style.display = "block")),
                    u && u(p)),
                    T = !1
                }
            }
        }
    }();
function HashMap() {
    var e = [];
    return e.size = function() {
        return this.length
    }
    ,
    e.isEmpty = function() {
        return 0 === this.length
    }
    ,
    e.containsKey = function(e) {
        e += "";
        for (var t = 0; t < this.length; t++)
            if (this[t].key === e)
                return t;
        return -1
    }
    ,
    e.get = function(e) {
        e += "";
        var t = this.containsKey(e);
        if (-1 < t)
            return this[t].value
    }
    ,
    e.put = function(e, t) {
        if (e += "",
        -1 !== this.containsKey(e))
            return this.get(e);
        this.push({
            key: e,
            value: t
        })
    }
    ,
    e.allKeys = function() {
        for (var e = [], t = 0; t < this.length; t++)
            e.push(this[t].key);
        return e
    }
    ,
    e.allIntKeys = function() {
        for (var e = [], t = 0; t < this.length; t++)
            e.push(parseInt(this[t].key));
        return e
    }
    ,
    e.remove = function(e) {
        e += "";
        var t = this.containsKey(e);
        -1 < t && this.splice(t, 1)
    }
    ,
    e
}
!function() {
    if ("undefined" != typeof window && !window.WebSocket) {
        var n = window.console;
        n && n.log && n.error || (n = {
            log: function() {},
            error: function() {}
        }),
        swfobject.hasFlashPlayerVersion("10.0.0") ? ("file:" == location.protocol && n.error("WARNING: web-socket-js doesn't work in file:///... URL unless you set Flash Security Settings properly. Open the page via Web server i.e. http://..."),
        WebSocket = function(e, t, n, r, i) {
            var o = this;
            o.__id = WebSocket.__nextId++,
            (WebSocket.__instances[o.__id] = o).readyState = WebSocket.CONNECTING,
            o.bufferedAmount = 0,
            o.__events = {},
            t ? "string" == typeof t && (t = [t]) : t = [],
            setTimeout(function() {
                WebSocket.__addTask(function() {
                    WebSocket.__flash.create(o.__id, e, t, n || null, r || 0, i || null)
                })
            }, 0)
        }
        ,
        WebSocket.prototype.send = function(e) {
            if (this.readyState == WebSocket.CONNECTING)
                throw "INVALID_STATE_ERR: Web Socket connection has not been established";
            var t = WebSocket.__flash.send(this.__id, encodeURIComponent(e));
            return t < 0 || (this.bufferedAmount += t,
            !1)
        }
        ,
        WebSocket.prototype.close = function() {
            this.readyState != WebSocket.CLOSED && this.readyState != WebSocket.CLOSING && (this.readyState = WebSocket.CLOSING,
            WebSocket.__flash.close(this.__id))
        }
        ,
        WebSocket.prototype.addEventListener = function(e, t, n) {
            e in this.__events || (this.__events[e] = []),
            this.__events[e].push(t)
        }
        ,
        WebSocket.prototype.removeEventListener = function(e, t, n) {
            if (e in this.__events)
                for (var r = this.__events[e], i = r.length - 1; 0 <= i; --i)
                    if (r[i] === t) {
                        r.splice(i, 1);
                        break
                    }
        }
        ,
        WebSocket.prototype.dispatchEvent = function(e) {
            for (var t = this.__events[e.type] || [], n = 0; n < t.length; ++n)
                t[n](e);
            var r = this["on" + e.type];
            r && r(e)
        }
        ,
        WebSocket.prototype.__handleEvent = function(e) {
            var t;
            if ("readyState"in e && (this.readyState = e.readyState),
            "protocol"in e && (this.protocol = e.protocol),
            "open" == e.type || "error" == e.type)
                t = this.__createSimpleEvent(e.type);
            else if ("close" == e.type)
                t = this.__createSimpleEvent("close");
            else {
                if ("message" != e.type)
                    throw "unknown event type: " + e.type;
                var n = decodeURIComponent(e.message);
                t = this.__createMessageEvent("message", n)
            }
            this.dispatchEvent(t)
        }
        ,
        WebSocket.prototype.__createSimpleEvent = function(e) {
            if (document.createEvent && window.Event) {
                var t = document.createEvent("Event");
                return t.initEvent(e, !1, !1),
                t
            }
            return {
                type: e,
                bubbles: !1,
                cancelable: !1
            }
        }
        ,
        WebSocket.prototype.__createMessageEvent = function(e, t) {
            if (document.createEvent && window.MessageEvent && !window.opera) {
                var n = document.createEvent("MessageEvent");
                return n.initMessageEvent("message", !1, !1, t, null, null, window, null),
                n
            }
            return {
                type: e,
                data: t,
                bubbles: !1,
                cancelable: !1
            }
        }
        ,
        WebSocket.CONNECTING = 0,
        WebSocket.OPEN = 1,
        WebSocket.CLOSING = 2,
        WebSocket.CLOSED = 3,
        WebSocket.__flash = null,
        WebSocket.__instances = {},
        WebSocket.__tasks = [],
        WebSocket.__nextId = 0,
        WebSocket.loadFlashPolicyFile = function(e) {
            WebSocket.__addTask(function() {
                WebSocket.__flash.loadManualPolicyFile(e)
            })
        }
        ,
        WebSocket.__initialize = function() {
            if (!WebSocket.__flash)
                if (WebSocket.__swfLocation && (window.WEB_SOCKET_SWF_LOCATION = WebSocket.__swfLocation),
                window.WEB_SOCKET_SWF_LOCATION) {
                    var e = document.createElement("div");
                    e.id = "webSocketContainer",
                    e.style.position = "absolute",
                    WebSocket.__isFlashLite() ? (e.style.left = "0px",
                    e.style.top = "0px") : (e.style.left = "-100px",
                    e.style.top = "-100px");
                    var t = document.createElement("div");
                    t.id = "webSocketFlash",
                    e.appendChild(t),
                    document.body.appendChild(e),
                    swfobject.embedSWF(WEB_SOCKET_SWF_LOCATION, "webSocketFlash", "1", "1", "10.0.0", null, null, {
                        hasPriority: !0,
                        swliveconnect: !0,
                        allowScriptAccess: "always"
                    }, null, function(e) {
                        e.success || n.error("[WebSocket] swfobject.embedSWF failed")
                    })
                } else
                    n.error("[WebSocket] set WEB_SOCKET_SWF_LOCATION to location of WebSocketMain.swf")
        }
        ,
        WebSocket.__onFlashInitialized = function() {
            setTimeout(function() {
                WebSocket.__flash = document.getElementById("webSocketFlash"),
                WebSocket.__flash.setCallerUrl(location.href),
                WebSocket.__flash.setDebug(!!window.WEB_SOCKET_DEBUG);
                for (var e = 0; e < WebSocket.__tasks.length; ++e)
                    WebSocket.__tasks[e]();
                WebSocket.__tasks = []
            }, 0)
        }
        ,
        WebSocket.__onFlashEvent = function() {
            return setTimeout(function() {
                try {
                    for (var e = WebSocket.__flash.receiveEvents(), t = 0; t < e.length; ++t)
                        WebSocket.__instances[e[t].webSocketId].__handleEvent(e[t])
                } catch (e) {
                    n.error(e)
                }
            }, 0),
            !0
        }
        ,
        WebSocket.__log = function(e) {
            n.log(decodeURIComponent(e))
        }
        ,
        WebSocket.__error = function(e) {
            n.error(decodeURIComponent(e))
        }
        ,
        WebSocket.__addTask = function(e) {
            WebSocket.__flash ? e() : WebSocket.__tasks.push(e)
        }
        ,
        WebSocket.__isFlashLite = function() {
            if (!window.navigator || !window.navigator.mimeTypes)
                return !1;
            var e = window.navigator.mimeTypes["application/x-shockwave-flash"];
            return !!(e && e.enabledPlugin && e.enabledPlugin.filename) && !!e.enabledPlugin.filename.match(/flashlite/i)
        }
        ,
        window.WEB_SOCKET_DISABLE_AUTO_INITIALIZATION || (window.addEventListener ? window.addEventListener("load", function() {
            WebSocket.__initialize()
        }, !1) : window.attachEvent("onload", function() {
            WebSocket.__initialize()
        }))) : n.error("Flash Player >= 10.0.0 is required.")
    }
}(),
function(e, o, a) {
    function t(e) {
        e && (o.Transport.apply(this, arguments),
        this.sendBuffer = [])
    }
    function n() {}
    e.XHR = t,
    o.util.inherit(t, o.Transport),
    t.prototype.open = function() {
        return this.socket.setBuffer(!1),
        this.onOpen(),
        this.get(),
        this.setCloseTimeout(),
        this
    }
    ,
    t.prototype.payload = function(e) {
        for (var t = [], n = 0, r = e.length; n < r; n++)
            t.push(o.parser.encodePacket(e[n]));
        this.send(o.parser.encodePayload(t))
    }
    ,
    t.prototype.send = function(e) {
        return this.post(e),
        this
    }
    ,
    t.prototype.post = function(e) {
        var t = this;
        this.socket.setBuffer(!0),
        this.sendXHR = this.request("POST"),
        a.XDomainRequest && this.sendXHR instanceof XDomainRequest ? this.sendXHR.onload = this.sendXHR.onerror = function() {
            this.onload = n,
            t.socket.setBuffer(!1)
        }
        : this.sendXHR.onreadystatechange = function() {
            4 == this.readyState && (this.onreadystatechange = n,
            t.posting = !1,
            200 == this.status ? t.socket.setBuffer(!1) : t.onClose())
        }
        ,
        this.sendXHR.send(e)
    }
    ,
    t.prototype.close = function() {
        return this.onClose(),
        this
    }
    ,
    t.prototype.request = function(e) {
        var t = o.util.request(this.socket.isXDomain())
          , n = o.util.query(this.socket.options.query, "t=" + +new Date);
        if (t.open(e || "GET", this.prepareUrl() + n, !0),
        "POST" == e)
            try {
                t.setRequestHeader ? t.setRequestHeader("Content-type", "text/plain;charset=UTF-8") : t.contentType = "text/plain"
            } catch (e) {}
        return t
    }
    ,
    t.prototype.scheme = function() {
        return this.socket.options.secure ? "https" : "http"
    }
    ,
    t.check = function(e, t) {
        try {
            var n = o.util.request(t)
              , r = a.XDomainRequest && n instanceof XDomainRequest
              , i = (e && e.options && e.options.secure ? "https:" : "http:") != a.location.protocol;
            if (n && (!r || !i))
                return !0
        } catch (e) {}
        return !1
    }
    ,
    t.xdomainCheck = function() {
        return t.check(null, !0)
    }
}("undefined" != typeof io0 ? io0.Transport : module.exports, "undefined" != typeof io0 ? io0 : module.parent.exports, this),
function(e, r) {
    function t(e) {
        r.Transport.XHR.apply(this, arguments)
    }
    e.htmlfile = t,
    r.util.inherit(t, r.Transport.XHR),
    t.prototype.name = "htmlfile",
    t.prototype.get = function() {
        this.doc = new (window[["Active"].concat("Object").join("X")])("htmlfile"),
        this.doc.open(),
        this.doc.write("<html></html>"),
        this.doc.close();
        var e = (this.doc.parentWindow.s = this).doc.createElement("div");
        e.className = "socketio",
        this.doc.body.appendChild(e),
        this.iframe = this.doc.createElement("iframe"),
        e.appendChild(this.iframe);
        var t = this
          , n = r.util.query(this.socket.options.query, "t=" + +new Date);
        this.iframe.src = this.prepareUrl() + n,
        r.util.on(window, "unload", function() {
            t.destroy()
        })
    }
    ,
    t.prototype._ = function(e, t) {
        this.onData(e);
        try {
            var n = t.getElementsByTagName("script")[0];
            n.parentNode.removeChild(n)
        } catch (e) {}
    }
    ,
    t.prototype.destroy = function() {
        if (this.iframe) {
            try {
                this.iframe.src = "about:blank"
            } catch (e) {}
            this.doc = null,
            this.iframe.parentNode.removeChild(this.iframe),
            this.iframe = null,
            CollectGarbage()
        }
    }
    ,
    t.prototype.close = function() {
        return this.destroy(),
        r.Transport.XHR.prototype.close.call(this)
    }
    ,
    t.check = function() {
        if ("undefined" != typeof window && ["Active"].concat("Object").join("X")in window)
            try {
                return new (window[["Active"].concat("Object").join("X")])("htmlfile") && r.Transport.XHR.check()
            } catch (e) {}
        return !1
    }
    ,
    t.xdomainCheck = function() {
        return !1
    }
    ,
    r.transports.push("htmlfile")
}("undefined" != typeof io0 ? io0.Transport : module.exports, "undefined" != typeof io0 ? io0 : module.parent.exports),
function(e, r, t) {
    function n() {
        r.Transport.XHR.apply(this, arguments)
    }
    function i() {}
    e["xhr-polling"] = n,
    r.util.inherit(n, r.Transport.XHR),
    r.util.merge(n, r.Transport.XHR),
    n.prototype.name = "xhr-polling",
    n.prototype.open = function() {
        return r.Transport.XHR.prototype.open.call(this),
        !1
    }
    ,
    n.prototype.get = function() {
        if (this.open) {
            var e = this;
            this.xhr = this.request(),
            t.XDomainRequest && this.xhr instanceof XDomainRequest ? (this.xhr.onload = function() {
                this.onload = i,
                this.onerror = i,
                e.onData(this.responseText),
                e.get()
            }
            ,
            this.xhr.onerror = function() {
                e.onClose()
            }
            ) : this.xhr.onreadystatechange = function() {
                4 == this.readyState && (this.onreadystatechange = i,
                200 == this.status ? (e.onData(this.responseText),
                e.get()) : e.onClose())
            }
            ,
            this.xhr.send(null)
        }
    }
    ,
    n.prototype.onClose = function() {
        if (r.Transport.XHR.prototype.onClose.call(this),
        this.xhr) {
            this.xhr.onreadystatechange = this.xhr.onload = this.xhr.onerror = i;
            try {
                this.xhr.abort()
            } catch (e) {}
            this.xhr = null
        }
    }
    ,
    n.prototype.ready = function(e, t) {
        var n = this;
        r.util.defer(function() {
            t.call(n)
        })
    }
    ,
    r.transports.push("xhr-polling")
}("undefined" != typeof io0 ? io0.Transport : module.exports, "undefined" != typeof io0 ? io0 : module.parent.exports, this),
function(e, d, t) {
    var i = t.document && "MozAppearance"in t.document.documentElement.style;
    function n(e) {
        d.Transport["xhr-polling"].apply(this, arguments),
        this.index = d.j.length;
        var t = this;
        d.j.push(function(e) {
            t._(e)
        })
    }
    e["jsonp-polling"] = n,
    d.util.inherit(n, d.Transport["xhr-polling"]),
    n.prototype.name = "jsonp-polling",
    n.prototype.post = function(e) {
        var t = this
          , n = d.util.query(this.socket.options.query, "t=" + +new Date + "&i=" + this.index);
        if (!this.form) {
            var r, i = document.createElement("form"), o = document.createElement("textarea"), a = this.iframeId = "socketio_iframe_" + this.index;
            i.className = "socketio",
            i.style.position = "absolute",
            i.style.top = "0px",
            i.style.left = "0px",
            i.style.display = "none",
            i.target = a,
            i.method = "POST",
            i.setAttribute("accept-charset", "utf-8"),
            o.name = "d",
            i.appendChild(o),
            document.body.appendChild(i),
            this.form = i,
            this.area = o
        }
        function s() {
            c(),
            t.socket.setBuffer(!1)
        }
        function c() {
            t.iframe && t.form.removeChild(t.iframe);
            try {
                r = document.createElement('<iframe name="' + t.iframeId + '">')
            } catch (e) {
                (r = document.createElement("iframe")).name = t.iframeId
            }
            r.id = t.iframeId,
            t.form.appendChild(r),
            t.iframe = r
        }
        this.form.action = this.prepareUrl() + n,
        c(),
        this.area.value = d.JSON.stringify(e);
        try {
            this.form.submit()
        } catch (e) {}
        this.iframe.attachEvent ? r.onreadystatechange = function() {
            "complete" == t.iframe.readyState && s()
        }
        : this.iframe.onload = s,
        this.socket.setBuffer(!0)
    }
    ,
    n.prototype.get = function() {
        var e = this
          , t = document.createElement("script")
          , n = d.util.query(this.socket.options.query, "t=" + +new Date + "&i=" + this.index);
        this.script && (this.script.parentNode.removeChild(this.script),
        this.script = null),
        t.async = !0,
        t.src = this.prepareUrl() + n,
        t.onerror = function() {
            e.onClose()
        }
        ;
        var r = document.getElementsByTagName("script")[0];
        r.parentNode.insertBefore(t, r),
        this.script = t,
        i && setTimeout(function() {
            var e = document.createElement("iframe");
            document.body.appendChild(e),
            document.body.removeChild(e)
        }, 100)
    }
    ,
    n.prototype._ = function(e) {
        return this.onData(e),
        this.open && this.get(),
        this
    }
    ,
    n.prototype.ready = function(e, t) {
        var n = this;
        if (!i)
            return t.call(this);
        d.util.load(function() {
            t.call(n)
        })
    }
    ,
    n.check = function() {
        return "document"in t
    }
    ,
    n.xdomainCheck = function() {
        return !0
    }
    ,
    d.transports.push("jsonp-polling")
}("undefined" != typeof io0 ? io0.Transport : module.exports, "undefined" != typeof io0 ? io0 : module.parent.exports, this),
function(e) {
    if ("object" == typeof exports && "undefined" != typeof module)
        module.exports = e();
    else if ("function" == typeof define && define.amd)
        define([], e);
    else {
        ("undefined" != typeof window ? window : "undefined" != typeof global ? global : "undefined" != typeof self ? self : this).adapter = e()
    }
}(function() {
    return function o(a, s, c) {
        function d(n, e) {
            if (!s[n]) {
                if (!a[n]) {
                    var t = "function" == typeof require && require;
                    if (!e && t)
                        return t(n, !0);
                    if (u)
                        return u(n, !0);
                    var r = new Error("Cannot find module '" + n + "'");
                    throw r.code = "MODULE_NOT_FOUND",
                    r
                }
                var i = s[n] = {
                    exports: {}
                };
                a[n][0].call(i.exports, function(e) {
                    var t = a[n][1][e];
                    return d(t || e)
                }, i, i.exports, o, a, s, c)
            }
            return s[n].exports
        }
        for (var u = "function" == typeof require && require, e = 0; e < c.length; e++)
            d(c[e]);
        return d
    }({
        1: [function(e, t, n) {
            "use strict";
            var u = {
                generateIdentifier: function() {
                    return Math.random().toString(36).substr(2, 10)
                }
            };
            u.localCName = u.generateIdentifier(),
            u.splitLines = function(e) {
                return e.trim().split("\n").map(function(e) {
                    return e.trim()
                })
            }
            ,
            u.splitSections = function(e) {
                return e.split("\nm=").map(function(e, t) {
                    return (0 < t ? "m=" + e : e).trim() + "\r\n"
                })
            }
            ,
            u.matchPrefix = function(e, t) {
                return u.splitLines(e).filter(function(e) {
                    return 0 === e.indexOf(t)
                })
            }
            ,
            u.parseCandidate = function(e) {
                for (var t, n = {
                    foundation: (t = 0 === e.indexOf("a=candidate:") ? e.substring(12).split(" ") : e.substring(10).split(" "))[0],
                    component: t[1],
                    protocol: t[2].toLowerCase(),
                    priority: parseInt(t[3], 10),
                    ip: t[4],
                    port: parseInt(t[5], 10),
                    type: t[7]
                }, r = 8; r < t.length; r += 2)
                    switch (t[r]) {
                    case "raddr":
                        n.relatedAddress = t[r + 1];
                        break;
                    case "rport":
                        n.relatedPort = parseInt(t[r + 1], 10);
                        break;
                    case "tcptype":
                        n.tcpType = t[r + 1];
                        break;
                    default:
                        n[t[r]] = t[r + 1]
                    }
                return n
            }
            ,
            u.writeCandidate = function(e) {
                var t = [];
                t.push(e.foundation),
                t.push(e.component),
                t.push(e.protocol.toUpperCase()),
                t.push(e.priority),
                t.push(e.ip),
                t.push(e.port);
                var n = e.type;
                return t.push("typ"),
                t.push(n),
                "host" !== n && e.relatedAddress && e.relatedPort && (t.push("raddr"),
                t.push(e.relatedAddress),
                t.push("rport"),
                t.push(e.relatedPort)),
                e.tcpType && "tcp" === e.protocol.toLowerCase() && (t.push("tcptype"),
                t.push(e.tcpType)),
                "candidate:" + t.join(" ")
            }
            ,
            u.parseIceOptions = function(e) {
                return e.substr(14).split(" ")
            }
            ,
            u.parseRtpMap = function(e) {
                var t = e.substr(9).split(" ")
                  , n = {
                    payloadType: parseInt(t.shift(), 10)
                };
                return t = t[0].split("/"),
                n.name = t[0],
                n.clockRate = parseInt(t[1], 10),
                n.numChannels = 3 === t.length ? parseInt(t[2], 10) : 1,
                n
            }
            ,
            u.writeRtpMap = function(e) {
                var t = e.payloadType;
                return void 0 !== e.preferredPayloadType && (t = e.preferredPayloadType),
                "a=rtpmap:" + t + " " + e.name + "/" + e.clockRate + (1 !== e.numChannels ? "/" + e.numChannels : "") + "\r\n"
            }
            ,
            u.parseExtmap = function(e) {
                var t = e.substr(9).split(" ");
                return {
                    id: parseInt(t[0], 10),
                    direction: 0 < t[0].indexOf("/") ? t[0].split("/")[1] : "sendrecv",
                    uri: t[1]
                }
            }
            ,
            u.writeExtmap = function(e) {
                return "a=extmap:" + (e.id || e.preferredId) + (e.direction && "sendrecv" !== e.direction ? "/" + e.direction : "") + " " + e.uri + "\r\n"
            }
            ,
            u.parseFmtp = function(e) {
                for (var t, n = {}, r = e.substr(e.indexOf(" ") + 1).split(";"), i = 0; i < r.length; i++)
                    n[(t = r[i].trim().split("="))[0].trim()] = t[1];
                return n
            }
            ,
            u.writeFmtp = function(t) {
                var e = ""
                  , n = t.payloadType;
                if (void 0 !== t.preferredPayloadType && (n = t.preferredPayloadType),
                t.parameters && Object.keys(t.parameters).length) {
                    var r = [];
                    Object.keys(t.parameters).forEach(function(e) {
                        r.push(e + "=" + t.parameters[e])
                    }),
                    e += "a=fmtp:" + n + " " + r.join(";") + "\r\n"
                }
                return e
            }
            ,
            u.parseRtcpFb = function(e) {
                var t = e.substr(e.indexOf(" ") + 1).split(" ");
                return {
                    type: t.shift(),
                    parameter: t.join(" ")
                }
            }
            ,
            u.writeRtcpFb = function(e) {
                var t = ""
                  , n = e.payloadType;
                return void 0 !== e.preferredPayloadType && (n = e.preferredPayloadType),
                e.rtcpFeedback && e.rtcpFeedback.length && e.rtcpFeedback.forEach(function(e) {
                    t += "a=rtcp-fb:" + n + " " + e.type + (e.parameter && e.parameter.length ? " " + e.parameter : "") + "\r\n"
                }),
                t
            }
            ,
            u.parseSsrcMedia = function(e) {
                var t = e.indexOf(" ")
                  , n = {
                    ssrc: parseInt(e.substr(7, t - 7), 10)
                }
                  , r = e.indexOf(":", t);
                return -1 < r ? (n.attribute = e.substr(t + 1, r - t - 1),
                n.value = e.substr(r + 1)) : n.attribute = e.substr(t + 1),
                n
            }
            ,
            u.getMid = function(e) {
                var t = u.matchPrefix(e, "a=mid:")[0];
                if (t)
                    return t.substr(6)
            }
            ,
            u.parseFingerprint = function(e) {
                var t = e.substr(14).split(" ");
                return {
                    algorithm: t[0].toLowerCase(),
                    value: t[1]
                }
            }
            ,
            u.getDtlsParameters = function(e, t) {
                return {
                    role: "auto",
                    fingerprints: u.matchPrefix(e + t, "a=fingerprint:").map(u.parseFingerprint)
                }
            }
            ,
            u.writeDtlsParameters = function(e, t) {
                var n = "a=setup:" + t + "\r\n";
                return e.fingerprints.forEach(function(e) {
                    n += "a=fingerprint:" + e.algorithm + " " + e.value + "\r\n"
                }),
                n
            }
            ,
            u.getIceParameters = function(e, t) {
                var n = u.splitLines(e);
                return {
                    usernameFragment: (n = n.concat(u.splitLines(t))).filter(function(e) {
                        return 0 === e.indexOf("a=ice-ufrag:")
                    })[0].substr(12),
                    password: n.filter(function(e) {
                        return 0 === e.indexOf("a=ice-pwd:")
                    })[0].substr(10)
                }
            }
            ,
            u.writeIceParameters = function(e) {
                return "a=ice-ufrag:" + e.usernameFragment + "\r\na=ice-pwd:" + e.password + "\r\n"
            }
            ,
            u.parseRtpParameters = function(e) {
                for (var t = {
                    codecs: [],
                    headerExtensions: [],
                    fecMechanisms: [],
                    rtcp: []
                }, n = u.splitLines(e)[0].split(" "), r = 3; r < n.length; r++) {
                    var i = n[r]
                      , o = u.matchPrefix(e, "a=rtpmap:" + i + " ")[0];
                    if (o) {
                        var a = u.parseRtpMap(o)
                          , s = u.matchPrefix(e, "a=fmtp:" + i + " ");
                        switch (a.parameters = s.length ? u.parseFmtp(s[0]) : {},
                        a.rtcpFeedback = u.matchPrefix(e, "a=rtcp-fb:" + i + " ").map(u.parseRtcpFb),
                        t.codecs.push(a),
                        a.name.toUpperCase()) {
                        case "RED":
                        case "ULPFEC":
                            t.fecMechanisms.push(a.name.toUpperCase())
                        }
                    }
                }
                return u.matchPrefix(e, "a=extmap:").forEach(function(e) {
                    t.headerExtensions.push(u.parseExtmap(e))
                }),
                t
            }
            ,
            u.writeRtpDescription = function(e, t) {
                var n = "";
                n += "m=" + e + " ",
                n += 0 < t.codecs.length ? "9" : "0",
                n += " UDP/TLS/RTP/SAVPF ",
                n += t.codecs.map(function(e) {
                    return void 0 !== e.preferredPayloadType ? e.preferredPayloadType : e.payloadType
                }).join(" ") + "\r\n",
                n += "c=IN IP4 0.0.0.0\r\n",
                n += "a=rtcp:9 IN IP4 0.0.0.0\r\n",
                t.codecs.forEach(function(e) {
                    n += u.writeRtpMap(e),
                    n += u.writeFmtp(e),
                    n += u.writeRtcpFb(e)
                });
                var r = 0;
                return t.codecs.forEach(function(e) {
                    e.maxptime > r && (r = e.maxptime)
                }),
                0 < r && (n += "a=maxptime:" + r + "\r\n"),
                n += "a=rtcp-mux\r\n",
                t.headerExtensions.forEach(function(e) {
                    n += u.writeExtmap(e)
                }),
                n
            }
            ,
            u.parseRtpEncodingParameters = function(e) {
                var n, r = [], t = u.parseRtpParameters(e), i = -1 !== t.fecMechanisms.indexOf("RED"), o = -1 !== t.fecMechanisms.indexOf("ULPFEC"), a = u.matchPrefix(e, "a=ssrc:").map(function(e) {
                    return u.parseSsrcMedia(e)
                }).filter(function(e) {
                    return "cname" === e.attribute
                }), s = 0 < a.length && a[0].ssrc, c = u.matchPrefix(e, "a=ssrc-group:FID").map(function(e) {
                    var t = e.split(" ");
                    return t.shift(),
                    t.map(function(e) {
                        return parseInt(e, 10)
                    })
                });
                0 < c.length && 1 < c[0].length && c[0][0] === s && (n = c[0][1]),
                t.codecs.forEach(function(e) {
                    if ("RTX" === e.name.toUpperCase() && e.parameters.apt) {
                        var t = {
                            ssrc: s,
                            codecPayloadType: parseInt(e.parameters.apt, 10),
                            rtx: {
                                ssrc: n
                            }
                        };
                        r.push(t),
                        i && ((t = JSON.parse(JSON.stringify(t))).fec = {
                            ssrc: n,
                            mechanism: o ? "red+ulpfec" : "red"
                        },
                        r.push(t))
                    }
                }),
                0 === r.length && s && r.push({
                    ssrc: s
                });
                var d = u.matchPrefix(e, "b=");
                return d.length && (0 === d[0].indexOf("b=TIAS:") ? d = parseInt(d[0].substr(7), 10) : 0 === d[0].indexOf("b=AS:") && (d = parseInt(d[0].substr(5), 10)),
                r.forEach(function(e) {
                    e.maxBitrate = d
                })),
                r
            }
            ,
            u.parseRtcpParameters = function(e) {
                var t = {}
                  , n = u.matchPrefix(e, "a=ssrc:").map(function(e) {
                    return u.parseSsrcMedia(e)
                }).filter(function(e) {
                    return "cname" === e.attribute
                })[0];
                n && (t.cname = n.value,
                t.ssrc = n.ssrc);
                var r = u.matchPrefix(e, "a=rtcp-rsize");
                t.reducedSize = 0 < r.length,
                t.compound = 0 === r.length;
                var i = u.matchPrefix(e, "a=rtcp-mux");
                return t.mux = 0 < i.length,
                t
            }
            ,
            u.parseMsid = function(e) {
                var t, n = u.matchPrefix(e, "a=msid:");
                if (1 === n.length)
                    return {
                        stream: (t = n[0].substr(7).split(" "))[0],
                        track: t[1]
                    };
                var r = u.matchPrefix(e, "a=ssrc:").map(function(e) {
                    return u.parseSsrcMedia(e)
                }).filter(function(e) {
                    return "msid" === e.attribute
                });
                return 0 < r.length ? {
                    stream: (t = r[0].value.split(" "))[0],
                    track: t[1]
                } : void 0
            }
            ,
            u.writeSessionBoilerplate = function() {
                return "v=0\r\no=thisisadapterortc 8169639915646943137 2 IN IP4 127.0.0.1\r\ns=-\r\nt=0 0\r\n"
            }
            ,
            u.writeMediaSection = function(e, t, n, r) {
                var i = u.writeRtpDescription(e.kind, t);
                if (i += u.writeIceParameters(e.iceGatherer.getLocalParameters()),
                i += u.writeDtlsParameters(e.dtlsTransport.getLocalParameters(), "offer" === n ? "actpass" : "active"),
                i += "a=mid:" + e.mid + "\r\n",
                e.direction ? i += "a=" + e.direction + "\r\n" : e.rtpSender && e.rtpReceiver ? i += "a=sendrecv\r\n" : e.rtpSender ? i += "a=sendonly\r\n" : e.rtpReceiver ? i += "a=recvonly\r\n" : i += "a=inactive\r\n",
                e.rtpSender) {
                    var o = "msid:" + r.id + " " + e.rtpSender.track.id + "\r\n";
                    i += "a=" + o,
                    i += "a=ssrc:" + e.sendEncodingParameters[0].ssrc + " " + o,
                    e.sendEncodingParameters[0].rtx && (i += "a=ssrc:" + e.sendEncodingParameters[0].rtx.ssrc + " " + o,
                    i += "a=ssrc-group:FID " + e.sendEncodingParameters[0].ssrc + " " + e.sendEncodingParameters[0].rtx.ssrc + "\r\n")
                }
                return i += "a=ssrc:" + e.sendEncodingParameters[0].ssrc + " cname:" + u.localCName + "\r\n",
                e.rtpSender && e.sendEncodingParameters[0].rtx && (i += "a=ssrc:" + e.sendEncodingParameters[0].rtx.ssrc + " cname:" + u.localCName + "\r\n"),
                i
            }
            ,
            u.getDirection = function(e, t) {
                for (var n = u.splitLines(e), r = 0; r < n.length; r++)
                    switch (n[r]) {
                    case "a=sendrecv":
                    case "a=sendonly":
                    case "a=recvonly":
                    case "a=inactive":
                        return n[r].substr(2)
                    }
                return t ? u.getDirection(t) : "sendrecv"
            }
            ,
            u.getKind = function(e) {
                return u.splitLines(e)[0].split(" ")[0].substr(2)
            }
            ,
            u.isRejected = function(e) {
                return "0" === e.split(" ", 2)[1]
            }
            ,
            t.exports = u
        }
        , {}],
        2: [function(n, r, e) {
            (function(e) {
                "use strict";
                var t = n("./adapter_factory.js");
                r.exports = t({
                    window: e.window
                })
            }
            ).call(this, "undefined" != typeof global ? global : "undefined" != typeof self ? self : "undefined" != typeof window ? window : {})
        }
        , {
            "./adapter_factory.js": 3
        }],
        3: [function(u, e, t) {
            "use strict";
            e.exports = function(e) {
                var t = e && e.window
                  , n = u("./utils")
                  , r = n.log
                  , i = n.detectBrowser(t)
                  , o = {
                    browserDetails: i,
                    extractVersion: n.extractVersion,
                    disableLog: n.disableLog
                }
                  , a = u("./chrome/chrome_shim") || null
                  , s = u("./edge/edge_shim") || null
                  , c = u("./firefox/firefox_shim") || null
                  , d = u("./safari/safari_shim") || null;
                switch (i.browser) {
                case "chrome":
                    if (!a || !a.shimPeerConnection)
                        return r("Chrome shim is not included in this adapter release."),
                        o;
                    r("adapter.js shimming chrome."),
                    (o.browserShim = a).shimGetUserMedia(t),
                    a.shimMediaStream(t),
                    n.shimCreateObjectURL(t),
                    a.shimSourceObject(t),
                    a.shimPeerConnection(t),
                    a.shimOnTrack(t),
                    a.shimGetSendersWithDtmf(t);
                    break;
                case "firefox":
                    if (!c || !c.shimPeerConnection)
                        return r("Firefox shim is not included in this adapter release."),
                        o;
                    r("adapter.js shimming firefox."),
                    (o.browserShim = c).shimGetUserMedia(t),
                    n.shimCreateObjectURL(t),
                    c.shimSourceObject(t),
                    c.shimPeerConnection(t),
                    c.shimOnTrack(t);
                    break;
                case "edge":
                    if (!s || !s.shimPeerConnection)
                        return r("MS edge shim is not included in this adapter release."),
                        o;
                    r("adapter.js shimming edge."),
                    (o.browserShim = s).shimGetUserMedia(t),
                    n.shimCreateObjectURL(t),
                    s.shimPeerConnection(t),
                    s.shimReplaceTrack(t);
                    break;
                case "safari":
                    if (!d)
                        return r("Safari shim is not included in this adapter release."),
                        o;
                    r("adapter.js shimming safari."),
                    o.browserShim = d,
                    n.shimCreateObjectURL(t),
                    d.shimCallbacksAPI(t),
                    d.shimLocalStreamsAPI(t),
                    d.shimRemoteStreamsAPI(t),
                    d.shimGetUserMedia(t);
                    break;
                default:
                    r("Unsupported browser!")
                }
                return o
            }
        }
        , {
            "./chrome/chrome_shim": 4,
            "./edge/edge_shim": 6,
            "./firefox/firefox_shim": 9,
            "./safari/safari_shim": 11,
            "./utils": 12
        }],
        4: [function(e, t, n) {
            "use strict";
            var r = e("../utils.js")
              , i = r.log
              , o = {
                shimMediaStream: function(e) {
                    e.MediaStream = e.MediaStream || e.webkitMediaStream
                },
                shimOnTrack: function(o) {
                    "object" != typeof o || !o.RTCPeerConnection || "ontrack"in o.RTCPeerConnection.prototype || Object.defineProperty(o.RTCPeerConnection.prototype, "ontrack", {
                        get: function() {
                            return this._ontrack
                        },
                        set: function(e) {
                            var i = this;
                            this._ontrack && (this.removeEventListener("track", this._ontrack),
                            this.removeEventListener("addstream", this._ontrackpoly)),
                            this.addEventListener("track", this._ontrack = e),
                            this.addEventListener("addstream", this._ontrackpoly = function(r) {
                                r.stream.addEventListener("addtrack", function(t) {
                                    var e;
                                    e = o.RTCPeerConnection.prototype.getReceivers ? i.getReceivers().find(function(e) {
                                        return e.track.id === t.track.id
                                    }) : {
                                        track: t.track
                                    };
                                    var n = new Event("track");
                                    n.track = t.track,
                                    n.receiver = e,
                                    n.streams = [r.stream],
                                    i.dispatchEvent(n)
                                }),
                                r.stream.getTracks().forEach(function(t) {
                                    var e;
                                    e = o.RTCPeerConnection.prototype.getReceivers ? i.getReceivers().find(function(e) {
                                        return e.track.id === t.id
                                    }) : {
                                        track: t
                                    };
                                    var n = new Event("track");
                                    n.track = t,
                                    n.receiver = e,
                                    n.streams = [r.stream],
                                    this.dispatchEvent(n)
                                }
                                .bind(this))
                            }
                            .bind(this))
                        }
                    })
                },
                shimGetSendersWithDtmf: function(s) {
                    if ("object" == typeof s && s.RTCPeerConnection && !("getSenders"in s.RTCPeerConnection.prototype) && "createDTMFSender"in s.RTCPeerConnection.prototype) {
                        s.RTCPeerConnection.prototype.getSenders = function() {
                            return this._senders || []
                        }
                        ;
                        var n = s.RTCPeerConnection.prototype.addStream
                          , t = s.RTCPeerConnection.prototype.removeStream;
                        s.RTCPeerConnection.prototype.addTrack || (s.RTCPeerConnection.prototype.addTrack = function(t, e) {
                            var n = this;
                            if ("closed" === n.signalingState)
                                throw new DOMException("The RTCPeerConnection's signalingState is 'closed'.","InvalidStateError");
                            var r = [].slice.call(arguments, 1);
                            if (1 !== r.length || !r[0].getTracks().find(function(e) {
                                return e === t
                            }))
                                throw new DOMException("The adapter.js addTrack polyfill only supports a single  stream which is associated with the specified track.","NotSupportedError");
                            if (n._senders = n._senders || [],
                            n._senders.find(function(e) {
                                return e.track === t
                            }))
                                throw new DOMException("Track already exists.","InvalidAccessError");
                            n._streams = n._streams || {};
                            var i = n._streams[e.id];
                            if (i)
                                i.addTrack(t),
                                n.removeStream(i),
                                n.addStream(i);
                            else {
                                var o = new s.MediaStream([t]);
                                n._streams[e.id] = o,
                                n.addStream(o)
                            }
                            var a = {
                                track: t,
                                get dtmf() {
                                    return void 0 === this._dtmf && ("audio" === t.kind ? this._dtmf = n.createDTMFSender(t) : this._dtmf = null),
                                    this._dtmf
                                }
                            };
                            return n._senders.push(a),
                            a
                        }
                        ),
                        s.RTCPeerConnection.prototype.addStream = function(e) {
                            var t = this;
                            t._senders = t._senders || [],
                            n.apply(t, [e]),
                            e.getTracks().forEach(function(e) {
                                t._senders.push({
                                    track: e,
                                    get dtmf() {
                                        return void 0 === this._dtmf && ("audio" === e.kind ? this._dtmf = t.createDTMFSender(e) : this._dtmf = null),
                                        this._dtmf
                                    }
                                })
                            })
                        }
                        ,
                        s.RTCPeerConnection.prototype.removeStream = function(e) {
                            var n = this;
                            n._senders = n._senders || [],
                            t.apply(n, [e]),
                            e.getTracks().forEach(function(t) {
                                var e = n._senders.find(function(e) {
                                    return e.track === t
                                });
                                e && n._senders.splice(n._senders.indexOf(e), 1)
                            })
                        }
                    } else if ("object" == typeof s && s.RTCPeerConnection && "getSenders"in s.RTCPeerConnection.prototype && "createDTMFSender"in s.RTCPeerConnection.prototype && s.RTCRtpSender && !("dtmf"in s.RTCRtpSender.prototype)) {
                        var r = s.RTCPeerConnection.prototype.getSenders;
                        s.RTCPeerConnection.prototype.getSenders = function() {
                            var t = this
                              , e = r.apply(t, []);
                            return e.forEach(function(e) {
                                e._pc = t
                            }),
                            e
                        }
                        ,
                        Object.defineProperty(s.RTCRtpSender.prototype, "dtmf", {
                            get: function() {
                                return void 0 === this._dtmf && ("audio" === this.track.kind ? this._dtmf = this._pc.createDTMFSender(this.track) : this._dtmf = null),
                                this._dtmf
                            }
                        })
                    }
                },
                shimSourceObject: function(e) {
                    var n = e && e.URL;
                    "object" == typeof e && (!e.HTMLMediaElement || "srcObject"in e.HTMLMediaElement.prototype || Object.defineProperty(e.HTMLMediaElement.prototype, "srcObject", {
                        get: function() {
                            return this._srcObject
                        },
                        set: function(e) {
                            var t = this;
                            this._srcObject = e,
                            this.src && n.revokeObjectURL(this.src),
                            e ? (this.src = n.createObjectURL(e),
                            e.addEventListener("addtrack", function() {
                                t.src && n.revokeObjectURL(t.src),
                                t.src = n.createObjectURL(e)
                            }),
                            e.addEventListener("removetrack", function() {
                                t.src && n.revokeObjectURL(t.src),
                                t.src = n.createObjectURL(e)
                            })) : this.src = ""
                        }
                    }))
                },
                shimPeerConnection: function(n) {
                    var e = r.detectBrowser(n);
                    if (n.RTCPeerConnection) {
                        var o = n.RTCPeerConnection;
                        n.RTCPeerConnection = function(e, t) {
                            if (e && e.iceServers) {
                                for (var n = [], r = 0; r < e.iceServers.length; r++) {
                                    var i = e.iceServers[r];
                                    !i.hasOwnProperty("urls") && i.hasOwnProperty("url") ? (console.warn("RTCIceServer.url is deprecated! Use urls instead."),
                                    (i = JSON.parse(JSON.stringify(i))).urls = i.url,
                                    n.push(i)) : n.push(e.iceServers[r])
                                }
                                e.iceServers = n
                            }
                            return new o(e,t)
                        }
                        ,
                        n.RTCPeerConnection.prototype = o.prototype,
                        Object.defineProperty(n.RTCPeerConnection, "generateCertificate", {
                            get: function() {
                                return o.generateCertificate
                            }
                        })
                    } else
                        n.RTCPeerConnection = function(e, t) {
                            return i("PeerConnection"),
                            e && e.iceTransportPolicy && (e.iceTransports = e.iceTransportPolicy),
                            new n.webkitRTCPeerConnection(e,t)
                        }
                        ,
                        n.RTCPeerConnection.prototype = n.webkitRTCPeerConnection.prototype,
                        n.webkitRTCPeerConnection.generateCertificate && Object.defineProperty(n.RTCPeerConnection, "generateCertificate", {
                            get: function() {
                                return n.webkitRTCPeerConnection.generateCertificate
                            }
                        });
                    var s = n.RTCPeerConnection.prototype.getStats;
                    n.RTCPeerConnection.prototype.getStats = function(e, t, n) {
                        var r = this
                          , i = arguments;
                        if (0 < arguments.length && "function" == typeof e)
                            return s.apply(this, arguments);
                        if (0 === s.length && (0 === arguments.length || "function" != typeof e))
                            return s.apply(this, []);
                        var o = function(e) {
                            var r = {};
                            return e.result().forEach(function(t) {
                                var n = {
                                    id: t.id,
                                    timestamp: t.timestamp,
                                    type: {
                                        localcandidate: "local-candidate",
                                        remotecandidate: "remote-candidate"
                                    }[t.type] || t.type
                                };
                                t.names().forEach(function(e) {
                                    n[e] = t.stat(e)
                                }),
                                r[n.id] = n
                            }),
                            r
                        }
                          , a = function(t) {
                            return new Map(Object.keys(t).map(function(e) {
                                return [e, t[e]]
                            }))
                        };
                        if (2 <= arguments.length) {
                            return s.apply(this, [function(e) {
                                i[1](a(o(e)))
                            }
                            , e])
                        }
                        return new Promise(function(t, e) {
                            s.apply(r, [function(e) {
                                t(a(o(e)))
                            }
                            , e])
                        }
                        ).then(t, n)
                    }
                    ,
                    e.version < 51 && ["setLocalDescription", "setRemoteDescription", "addIceCandidate"].forEach(function(e) {
                        var i = n.RTCPeerConnection.prototype[e];
                        n.RTCPeerConnection.prototype[e] = function() {
                            var n = arguments
                              , r = this
                              , e = new Promise(function(e, t) {
                                i.apply(r, [n[0], e, t])
                            }
                            );
                            return n.length < 2 ? e : e.then(function() {
                                n[1].apply(null, [])
                            }, function(e) {
                                3 <= n.length && n[2].apply(null, [e])
                            })
                        }
                    }),
                    e.version < 52 && ["createOffer", "createAnswer"].forEach(function(e) {
                        var i = n.RTCPeerConnection.prototype[e];
                        n.RTCPeerConnection.prototype[e] = function() {
                            var n = this;
                            if (arguments.length < 1 || 1 === arguments.length && "object" == typeof arguments[0]) {
                                var r = 1 === arguments.length ? arguments[0] : void 0;
                                return new Promise(function(e, t) {
                                    i.apply(n, [e, t, r])
                                }
                                )
                            }
                            return i.apply(this, arguments)
                        }
                    }),
                    ["setLocalDescription", "setRemoteDescription", "addIceCandidate"].forEach(function(e) {
                        var t = n.RTCPeerConnection.prototype[e];
                        n.RTCPeerConnection.prototype[e] = function() {
                            return arguments[0] = new ("addIceCandidate" === e ? n.RTCIceCandidate : n.RTCSessionDescription)(arguments[0]),
                            t.apply(this, arguments)
                        }
                    });
                    var t = n.RTCPeerConnection.prototype.addIceCandidate;
                    n.RTCPeerConnection.prototype.addIceCandidate = function() {
                        return arguments[0] ? t.apply(this, arguments) : (arguments[1] && arguments[1].apply(null),
                        Promise.resolve())
                    }
                }
            };
            t.exports = {
                shimMediaStream: o.shimMediaStream,
                shimOnTrack: o.shimOnTrack,
                shimGetSendersWithDtmf: o.shimGetSendersWithDtmf,
                shimSourceObject: o.shimSourceObject,
                shimPeerConnection: o.shimPeerConnection,
                shimGetUserMedia: e("./getusermedia")
            }
        }
        , {
            "../utils.js": 12,
            "./getusermedia": 5
        }],
        5: [function(e, t, n) {
            "use strict";
            var o = e("../utils.js")
              , d = o.log;
            t.exports = function(e) {
                var a = o.detectBrowser(e)
                  , s = e && e.navigator
                  , c = function(i) {
                    if ("object" != typeof i || i.mandatory || i.optional)
                        return i;
                    var o = {};
                    return Object.keys(i).forEach(function(t) {
                        if ("require" !== t && "advanced" !== t && "mediaSource" !== t) {
                            var n = "object" == typeof i[t] ? i[t] : {
                                ideal: i[t]
                            };
                            void 0 !== n.exact && "number" == typeof n.exact && (n.min = n.max = n.exact);
                            var r = function(e, t) {
                                return e ? e + t.charAt(0).toUpperCase() + t.slice(1) : "deviceId" === t ? "sourceId" : t
                            };
                            if (void 0 !== n.ideal) {
                                o.optional = o.optional || [];
                                var e = {};
                                "number" == typeof n.ideal ? (e[r("min", t)] = n.ideal,
                                o.optional.push(e),
                                (e = {})[r("max", t)] = n.ideal) : e[r("", t)] = n.ideal,
                                o.optional.push(e)
                            }
                            void 0 !== n.exact && "number" != typeof n.exact ? (o.mandatory = o.mandatory || {},
                            o.mandatory[r("", t)] = n.exact) : ["min", "max"].forEach(function(e) {
                                void 0 !== n[e] && (o.mandatory = o.mandatory || {},
                                o.mandatory[r(e, t)] = n[e])
                            })
                        }
                    }),
                    i.advanced && (o.optional = (o.optional || []).concat(i.advanced)),
                    o
                }
                  , r = function(n, r) {
                    if ((n = JSON.parse(JSON.stringify(n))) && "object" == typeof n.audio) {
                        var e = function(e, t, n) {
                            t in e && !(n in e) && (e[n] = e[t],
                            delete e[t])
                        };
                        e((n = JSON.parse(JSON.stringify(n))).audio, "autoGainControl", "googAutoGainControl"),
                        e(n.audio, "noiseSuppression", "googNoiseSuppression"),
                        n.audio = c(n.audio)
                    }
                    if (n && "object" == typeof n.video) {
                        var i = n.video.facingMode;
                        i = i && ("object" == typeof i ? i : {
                            ideal: i
                        });
                        var o, t = a.version < 61;
                        if (i && ("user" === i.exact || "environment" === i.exact || "user" === i.ideal || "environment" === i.ideal) && (!s.mediaDevices.getSupportedConstraints || !s.mediaDevices.getSupportedConstraints().facingMode || t))
                            if (delete n.video.facingMode,
                            "environment" === i.exact || "environment" === i.ideal ? o = ["back", "rear"] : "user" !== i.exact && "user" !== i.ideal || (o = ["front"]),
                            o)
                                return s.mediaDevices.enumerateDevices().then(function(e) {
                                    var t = (e = e.filter(function(e) {
                                        return "videoinput" === e.kind
                                    })).find(function(t) {
                                        return o.some(function(e) {
                                            return -1 !== t.label.toLowerCase().indexOf(e)
                                        })
                                    });
                                    return !t && e.length && -1 !== o.indexOf("back") && (t = e[e.length - 1]),
                                    t && (n.video.deviceId = i.exact ? {
                                        exact: t.deviceId
                                    } : {
                                        ideal: t.deviceId
                                    }),
                                    n.video = c(n.video),
                                    d("chrome: " + JSON.stringify(n)),
                                    r(n)
                                });
                        n.video = c(n.video)
                    }
                    return d("chrome: " + JSON.stringify(n)),
                    r(n)
                }
                  , i = function(e) {
                    return {
                        name: {
                            PermissionDeniedError: "NotAllowedError",
                            InvalidStateError: "NotReadableError",
                            DevicesNotFoundError: "NotFoundError",
                            ConstraintNotSatisfiedError: "OverconstrainedError",
                            TrackStartError: "NotReadableError",
                            MediaDeviceFailedDueToShutdown: "NotReadableError",
                            MediaDeviceKillSwitchOn: "NotReadableError"
                        }[e.name] || e.name,
                        message: e.message,
                        constraint: e.constraintName,
                        toString: function() {
                            return this.name + (this.message && ": ") + this.message
                        }
                    }
                };
                s.getUserMedia = function(e, t, n) {
                    r(e, function(e) {
                        s.webkitGetUserMedia(e, t, function(e) {
                            n(i(e))
                        })
                    })
                }
                ;
                var t = function(n) {
                    return new Promise(function(e, t) {
                        s.getUserMedia(n, e, t)
                    }
                    )
                };
                if (s.mediaDevices || (s.mediaDevices = {
                    getUserMedia: t,
                    enumerateDevices: function() {
                        return new Promise(function(t) {
                            var n = {
                                audio: "audioinput",
                                video: "videoinput"
                            };
                            return e.MediaStreamTrack.getSources(function(e) {
                                t(e.map(function(e) {
                                    return {
                                        label: e.label,
                                        kind: n[e.kind],
                                        deviceId: e.id,
                                        groupId: ""
                                    }
                                }))
                            })
                        }
                        )
                    },
                    getSupportedConstraints: function() {
                        return {
                            deviceId: !0,
                            echoCancellation: !0,
                            facingMode: !0,
                            frameRate: !0,
                            height: !0,
                            width: !0
                        }
                    }
                }),
                s.mediaDevices.getUserMedia) {
                    var n = s.mediaDevices.getUserMedia.bind(s.mediaDevices);
                    s.mediaDevices.getUserMedia = function(e) {
                        return r(e, function(t) {
                            return n(t).then(function(e) {
                                if (t.audio && !e.getAudioTracks().length || t.video && !e.getVideoTracks().length)
                                    throw e.getTracks().forEach(function(e) {
                                        e.stop()
                                    }),
                                    new DOMException("","NotFoundError");
                                return e
                            }, function(e) {
                                return Promise.reject(i(e))
                            })
                        })
                    }
                } else
                    s.mediaDevices.getUserMedia = function(e) {
                        return t(e)
                    }
                    ;
                void 0 === s.mediaDevices.addEventListener && (s.mediaDevices.addEventListener = function() {
                    d("Dummy mediaDevices.addEventListener called.")
                }
                ),
                void 0 === s.mediaDevices.removeEventListener && (s.mediaDevices.removeEventListener = function() {
                    d("Dummy mediaDevices.removeEventListener called.")
                }
                )
            }
        }
        , {
            "../utils.js": 12
        }],
        6: [function(e, t, n) {
            "use strict";
            var r = e("../utils")
              , i = e("./rtcpeerconnection_shim");
            t.exports = {
                shimGetUserMedia: e("./getusermedia"),
                shimPeerConnection: function(e) {
                    var t = r.detectBrowser(e);
                    if (e.RTCIceGatherer && (e.RTCIceCandidate || (e.RTCIceCandidate = function(e) {
                        return e
                    }
                    ),
                    e.RTCSessionDescription || (e.RTCSessionDescription = function(e) {
                        return e
                    }
                    ),
                    t.version < 15025)) {
                        var n = Object.getOwnPropertyDescriptor(e.MediaStreamTrack.prototype, "enabled");
                        Object.defineProperty(e.MediaStreamTrack.prototype, "enabled", {
                            set: function(e) {
                                n.set.call(this, e);
                                var t = new Event("enabled");
                                t.enabled = e,
                                this.dispatchEvent(t)
                            }
                        })
                    }
                    e.RTCPeerConnection = i(e, t.version)
                },
                shimReplaceTrack: function(e) {
                    !e.RTCRtpSender || "replaceTrack"in e.RTCRtpSender.prototype || (e.RTCRtpSender.prototype.replaceTrack = e.RTCRtpSender.prototype.setTrack)
                }
            }
        }
        , {
            "../utils": 12,
            "./getusermedia": 7,
            "./rtcpeerconnection_shim": 8
        }],
        7: [function(e, t, n) {
            "use strict";
            t.exports = function(e) {
                var t = e && e.navigator
                  , n = t.mediaDevices.getUserMedia.bind(t.mediaDevices);
                t.mediaDevices.getUserMedia = function(e) {
                    return n(e).catch(function(e) {
                        return Promise.reject({
                            name: {
                                PermissionDeniedError: "NotAllowedError"
                            }[(t = e).name] || t.name,
                            message: t.message,
                            constraint: t.constraint,
                            toString: function() {
                                return this.name
                            }
                        });
                        var t
                    })
                }
            }
        }
        , {}],
        8: [function(e, t, n) {
            "use strict";
            var A = e("sdp");
            function m(d, u) {
                var p = {
                    codecs: [],
                    headerExtensions: [],
                    fecMechanisms: []
                }
                  , l = function(e, t) {
                    e = parseInt(e, 10);
                    for (var n = 0; n < t.length; n++)
                        if (t[n].payloadType === e || t[n].preferredPayloadType === e)
                            return t[n]
                };
                return d.codecs.forEach(function(n) {
                    for (var e = 0; e < u.codecs.length; e++) {
                        var t = u.codecs[e];
                        if (n.name.toLowerCase() === t.name.toLowerCase() && n.clockRate === t.clockRate) {
                            if ("rtx" === n.name.toLowerCase() && n.parameters && t.parameters.apt && (r = n,
                            i = t,
                            o = d.codecs,
                            a = u.codecs,
                            c = s = void 0,
                            s = l(r.parameters.apt, o),
                            c = l(i.parameters.apt, a),
                            !s || !c || s.name.toLowerCase() !== c.name.toLowerCase()))
                                continue;
                            (t = JSON.parse(JSON.stringify(t))).numChannels = Math.min(n.numChannels, t.numChannels),
                            p.codecs.push(t),
                            t.rtcpFeedback = t.rtcpFeedback.filter(function(e) {
                                for (var t = 0; t < n.rtcpFeedback.length; t++)
                                    if (n.rtcpFeedback[t].type === e.type && n.rtcpFeedback[t].parameter === e.parameter)
                                        return !0;
                                return !1
                            });
                            break
                        }
                    }
                    var r, i, o, a, s, c
                }),
                d.headerExtensions.forEach(function(e) {
                    for (var t = 0; t < u.headerExtensions.length; t++) {
                        var n = u.headerExtensions[t];
                        if (e.uri === n.uri) {
                            p.headerExtensions.push(n);
                            break
                        }
                    }
                }),
                p
            }
            function a(e, t, n) {
                return -1 !== {
                    offer: {
                        setLocalDescription: ["stable", "have-local-offer"],
                        setRemoteDescription: ["stable", "have-remote-offer"]
                    },
                    answer: {
                        setLocalDescription: ["have-remote-offer", "have-local-pranswer"],
                        setRemoteDescription: ["have-local-offer", "have-remote-pranswer"]
                    }
                }[t][e].indexOf(n)
            }
            t.exports = function(x, O) {
                var e = function(e) {
                    var t, r, i, n = this, o = document.createDocumentFragment();
                    if (["addEventListener", "removeEventListener", "dispatchEvent"].forEach(function(e) {
                        n[e] = o[e].bind(o)
                    }),
                    this.needNegotiation = !1,
                    this.onicecandidate = null,
                    this.onaddstream = null,
                    this.ontrack = null,
                    this.onremovestream = null,
                    this.onsignalingstatechange = null,
                    this.oniceconnectionstatechange = null,
                    this.onicegatheringstatechange = null,
                    this.onnegotiationneeded = null,
                    this.ondatachannel = null,
                    this.canTrickleIceCandidates = null,
                    this.localStreams = [],
                    this.remoteStreams = [],
                    this.getLocalStreams = function() {
                        return n.localStreams
                    }
                    ,
                    this.getRemoteStreams = function() {
                        return n.remoteStreams
                    }
                    ,
                    this.localDescription = new x.RTCSessionDescription({
                        type: "",
                        sdp: ""
                    }),
                    this.remoteDescription = new x.RTCSessionDescription({
                        type: "",
                        sdp: ""
                    }),
                    this.signalingState = "stable",
                    this.iceConnectionState = "new",
                    this.iceGatheringState = "new",
                    this.iceOptions = {
                        gatherPolicy: "all",
                        iceServers: []
                    },
                    e && e.iceTransportPolicy)
                        switch (e.iceTransportPolicy) {
                        case "all":
                        case "relay":
                            this.iceOptions.gatherPolicy = e.iceTransportPolicy
                        }
                    this.usingBundle = e && "max-bundle" === e.bundlePolicy,
                    e && e.iceServers && (this.iceOptions.iceServers = (t = e.iceServers,
                    r = O,
                    i = !1,
                    (t = JSON.parse(JSON.stringify(t))).filter(function(e) {
                        if (e && (e.urls || e.url)) {
                            var t = e.urls || e.url;
                            e.url && !e.urls && console.warn("RTCIceServer.url is deprecated! Use urls instead.");
                            var n = "string" == typeof t;
                            return n && (t = [t]),
                            t = t.filter(function(e) {
                                return 0 !== e.indexOf("turn:") || -1 === e.indexOf("transport=udp") || -1 !== e.indexOf("turn:[") || i ? 0 === e.indexOf("stun:") && 14393 <= r : i = !0
                            }),
                            delete e.url,
                            e.urls = n ? t[0] : t,
                            !!t.length
                        }
                        return !1
                    }))),
                    this._config = e || {},
                    this.transceivers = [],
                    this._localIceCandidatesBuffer = []
                };
                return e.prototype._emitGatheringStateChange = function() {
                    var e = new Event("icegatheringstatechange");
                    this.dispatchEvent(e),
                    null !== this.onicegatheringstatechange && this.onicegatheringstatechange(e)
                }
                ,
                e.prototype._emitBufferedCandidates = function() {
                    var n = this
                      , r = A.splitSections(n.localDescription.sdp);
                    this._localIceCandidatesBuffer.forEach(function(e) {
                        if (!e.candidate || 0 === Object.keys(e.candidate).length)
                            for (var t = 1; t < r.length; t++)
                                -1 === r[t].indexOf("\r\na=end-of-candidates\r\n") && (r[t] += "a=end-of-candidates\r\n");
                        else
                            r[e.candidate.sdpMLineIndex + 1] += "a=" + e.candidate.candidate + "\r\n";
                        (n.localDescription.sdp = r.join(""),
                        n.dispatchEvent(e),
                        null !== n.onicecandidate && n.onicecandidate(e),
                        e.candidate || "complete" === n.iceGatheringState) || n.transceivers.every(function(e) {
                            return e.iceGatherer && "completed" === e.iceGatherer.state
                        }) && "complete" !== n.iceGatheringStateChange && (n.iceGatheringState = "complete",
                        n._emitGatheringStateChange())
                    }),
                    this._localIceCandidatesBuffer = []
                }
                ,
                e.prototype.getConfiguration = function() {
                    return this._config
                }
                ,
                e.prototype._createTransceiver = function(e) {
                    var t = 0 < this.transceivers.length
                      , n = {
                        track: null,
                        iceGatherer: null,
                        iceTransport: null,
                        dtlsTransport: null,
                        localCapabilities: null,
                        remoteCapabilities: null,
                        rtpSender: null,
                        rtpReceiver: null,
                        kind: e,
                        mid: null,
                        sendEncodingParameters: null,
                        recvEncodingParameters: null,
                        stream: null,
                        wantReceive: !0
                    };
                    if (this.usingBundle && t)
                        n.iceTransport = this.transceivers[0].iceTransport,
                        n.dtlsTransport = this.transceivers[0].dtlsTransport;
                    else {
                        var r = this._createIceAndDtlsTransports();
                        n.iceTransport = r.iceTransport,
                        n.dtlsTransport = r.dtlsTransport
                    }
                    return this.transceivers.push(n),
                    n
                }
                ,
                e.prototype.addTrack = function(e, t) {
                    for (var n, r = 0; r < this.transceivers.length; r++)
                        this.transceivers[r].track || this.transceivers[r].kind !== e.kind || (n = this.transceivers[r]);
                    return n || (n = this._createTransceiver(e.kind)),
                    n.track = e,
                    n.stream = t,
                    n.rtpSender = new x.RTCRtpSender(e,n.dtlsTransport),
                    this._maybeFireNegotiationNeeded(),
                    n.rtpSender
                }
                ,
                e.prototype.addStream = function(t) {
                    var n = this;
                    if (15025 <= O)
                        this.localStreams.push(t),
                        t.getTracks().forEach(function(e) {
                            n.addTrack(e, t)
                        });
                    else {
                        var r = t.clone();
                        t.getTracks().forEach(function(e, t) {
                            var n = r.getTracks()[t];
                            e.addEventListener("enabled", function(e) {
                                n.enabled = e.enabled
                            })
                        }),
                        r.getTracks().forEach(function(e) {
                            n.addTrack(e, r)
                        }),
                        this.localStreams.push(r)
                    }
                    this._maybeFireNegotiationNeeded()
                }
                ,
                e.prototype.removeStream = function(e) {
                    var t = this.localStreams.indexOf(e);
                    -1 < t && (this.localStreams.splice(t, 1),
                    this._maybeFireNegotiationNeeded())
                }
                ,
                e.prototype.getSenders = function() {
                    return this.transceivers.filter(function(e) {
                        return !!e.rtpSender
                    }).map(function(e) {
                        return e.rtpSender
                    })
                }
                ,
                e.prototype.getReceivers = function() {
                    return this.transceivers.filter(function(e) {
                        return !!e.rtpReceiver
                    }).map(function(e) {
                        return e.rtpReceiver
                    })
                }
                ,
                e.prototype._createIceGatherer = function(a, s) {
                    var c = this
                      , d = new x.RTCIceGatherer(c.iceOptions);
                    return d.onlocalcandidate = function(e) {
                        var t = new Event("icecandidate");
                        t.candidate = {
                            sdpMid: a,
                            sdpMLineIndex: s
                        };
                        var n = e.candidate
                          , r = !n || 0 === Object.keys(n).length;
                        r ? void 0 === d.state && (d.state = "completed") : (n.component = 1,
                        t.candidate.candidate = A.writeCandidate(n));
                        var i = A.splitSections(c.localDescription.sdp);
                        i[t.candidate.sdpMLineIndex + 1] += r ? "a=end-of-candidates\r\n" : "a=" + t.candidate.candidate + "\r\n",
                        c.localDescription.sdp = i.join("");
                        var o = (c._pendingOffer ? c._pendingOffer : c.transceivers).every(function(e) {
                            return e.iceGatherer && "completed" === e.iceGatherer.state
                        });
                        switch (c.iceGatheringState) {
                        case "new":
                            r || c._localIceCandidatesBuffer.push(t),
                            r && o && c._localIceCandidatesBuffer.push(new Event("icecandidate"));
                            break;
                        case "gathering":
                            c._emitBufferedCandidates(),
                            r || (c.dispatchEvent(t),
                            null !== c.onicecandidate && c.onicecandidate(t)),
                            o && (c.dispatchEvent(new Event("icecandidate")),
                            null !== c.onicecandidate && c.onicecandidate(new Event("icecandidate")),
                            c.iceGatheringState = "complete",
                            c._emitGatheringStateChange())
                        }
                    }
                    ,
                    d
                }
                ,
                e.prototype._createIceAndDtlsTransports = function() {
                    var e = this
                      , t = new x.RTCIceTransport(null);
                    t.onicestatechange = function() {
                        e._updateConnectionState()
                    }
                    ;
                    var n = new x.RTCDtlsTransport(t);
                    return n.ondtlsstatechange = function() {
                        e._updateConnectionState()
                    }
                    ,
                    n.onerror = function() {
                        Object.defineProperty(n, "state", {
                            value: "failed",
                            writable: !0
                        }),
                        e._updateConnectionState()
                    }
                    ,
                    {
                        iceTransport: t,
                        dtlsTransport: n
                    }
                }
                ,
                e.prototype._disposeIceAndDtlsTransports = function(e) {
                    var t = this.transceivers[e].iceGatherer;
                    t && (delete t.onlocalcandidate,
                    delete this.transceivers[e].iceGatherer);
                    var n = this.transceivers[e].iceTransport;
                    n && (delete n.onicestatechange,
                    delete this.transceivers[e].iceTransport);
                    var r = this.transceivers[e].dtlsTransport;
                    r && (delete r.ondtlssttatechange,
                    delete r.onerror,
                    delete this.transceivers[e].dtlsTransport)
                }
                ,
                e.prototype._transceive = function(e, t, n) {
                    var r = m(e.localCapabilities, e.remoteCapabilities);
                    t && e.rtpSender && (r.encodings = e.sendEncodingParameters,
                    r.rtcp = {
                        cname: A.localCName,
                        compound: e.rtcpParameters.compound
                    },
                    e.recvEncodingParameters.length && (r.rtcp.ssrc = e.recvEncodingParameters[0].ssrc),
                    e.rtpSender.send(r)),
                    n && e.rtpReceiver && ("video" === e.kind && e.recvEncodingParameters && O < 15019 && e.recvEncodingParameters.forEach(function(e) {
                        delete e.rtx
                    }),
                    r.encodings = e.recvEncodingParameters,
                    r.rtcp = {
                        cname: e.rtcpParameters.cname,
                        compound: e.rtcpParameters.compound
                    },
                    e.sendEncodingParameters.length && (r.rtcp.ssrc = e.sendEncodingParameters[0].ssrc),
                    e.rtpReceiver.receive(r))
                }
                ,
                e.prototype.setLocalDescription = function(e) {
                    var t, p, l = this;
                    if (!a("setLocalDescription", e.type, this.signalingState)) {
                        var n = new Error("Can not set local " + e.type + " in state " + this.signalingState);
                        return n.name = "InvalidStateError",
                        2 < arguments.length && "function" == typeof arguments[2] && x.setTimeout(arguments[2], 0, n),
                        Promise.reject(n)
                    }
                    if ("offer" === e.type)
                        this._pendingOffer && (t = A.splitSections(e.sdp),
                        p = t.shift(),
                        t.forEach(function(e, t) {
                            var n = A.parseRtpParameters(e);
                            l._pendingOffer[t].localCapabilities = n
                        }),
                        this.transceivers = this._pendingOffer,
                        delete this._pendingOffer);
                    else if ("answer" === e.type) {
                        t = A.splitSections(l.remoteDescription.sdp),
                        p = t.shift();
                        var f = 0 < A.matchPrefix(p, "a=ice-lite").length;
                        t.forEach(function(e, t) {
                            var n = l.transceivers[t]
                              , r = n.iceGatherer
                              , i = n.iceTransport
                              , o = n.dtlsTransport
                              , a = n.localCapabilities
                              , s = n.remoteCapabilities;
                            if (!A.isRejected(e) && !n.isDatachannel) {
                                var c = A.getIceParameters(e, p)
                                  , d = A.getDtlsParameters(e, p);
                                f && (d.role = "server"),
                                l.usingBundle && 0 !== t || (i.start(r, c, f ? "controlling" : "controlled"),
                                o.start(d));
                                var u = m(a, s);
                                l._transceive(n, 0 < u.codecs.length, !1)
                            }
                        })
                    }
                    switch (this.localDescription = {
                        type: e.type,
                        sdp: e.sdp
                    },
                    e.type) {
                    case "offer":
                        this._updateSignalingState("have-local-offer");
                        break;
                    case "answer":
                        this._updateSignalingState("stable");
                        break;
                    default:
                        throw new TypeError('unsupported type "' + e.type + '"')
                    }
                    var r = 1 < arguments.length && "function" == typeof arguments[1];
                    if (r) {
                        var i = arguments[1];
                        x.setTimeout(function() {
                            i(),
                            "new" === l.iceGatheringState && (l.iceGatheringState = "gathering",
                            l._emitGatheringStateChange()),
                            l._emitBufferedCandidates()
                        }, 0)
                    }
                    var o = Promise.resolve();
                    return o.then(function() {
                        r || ("new" === l.iceGatheringState && (l.iceGatheringState = "gathering",
                        l._emitGatheringStateChange()),
                        x.setTimeout(l._emitBufferedCandidates.bind(l), 500))
                    }),
                    o
                }
                ,
                e.prototype.setRemoteDescription = function(T) {
                    var w = this;
                    if (!a("setRemoteDescription", T.type, this.signalingState)) {
                        var e = new Error("Can not set remote " + T.type + " in state " + this.signalingState);
                        return e.name = "InvalidStateError",
                        2 < arguments.length && "function" == typeof arguments[2] && x.setTimeout(arguments[2], 0, e),
                        Promise.reject(e)
                    }
                    var L = {}
                      , R = []
                      , t = A.splitSections(T.sdp)
                      , _ = t.shift()
                      , P = 0 < A.matchPrefix(_, "a=ice-lite").length
                      , I = 0 < A.matchPrefix(_, "a=group:BUNDLE ").length;
                    this.usingBundle = I;
                    var n = A.matchPrefix(_, "a=ice-options:")[0];
                    switch (this.canTrickleIceCandidates = !!n && 0 <= n.substr(14).split(" ").indexOf("trickle"),
                    t.forEach(function(e, t) {
                        var n = A.splitLines(e)
                          , r = A.getKind(e)
                          , i = A.isRejected(e)
                          , o = n[0].substr(2).split(" ")[2]
                          , a = A.getDirection(e, _)
                          , s = A.parseMsid(e)
                          , c = A.getMid(e) || A.generateIdentifier();
                        if ("application" !== r || "DTLS/SCTP" !== o) {
                            var d, u, p, l, f, m, h, g, v, y, S, b = A.parseRtpParameters(e);
                            i || (y = A.getIceParameters(e, _),
                            (S = A.getDtlsParameters(e, _)).role = "client"),
                            h = A.parseRtpEncodingParameters(e);
                            var C = A.parseRtcpParameters(e)
                              , E = 0 < A.matchPrefix(e, "a=end-of-candidates", _).length
                              , k = A.matchPrefix(e, "a=candidate:").map(function(e) {
                                return A.parseCandidate(e)
                            }).filter(function(e) {
                                return "1" === e.component || 1 === e.component
                            });
                            ("offer" === T.type || "answer" === T.type) && !i && I && 0 < t && w.transceivers[t] && (w._disposeIceAndDtlsTransports(t),
                            w.transceivers[t].iceGatherer = w.transceivers[0].iceGatherer,
                            w.transceivers[t].iceTransport = w.transceivers[0].iceTransport,
                            w.transceivers[t].dtlsTransport = w.transceivers[0].dtlsTransport,
                            w.transceivers[t].rtpSender && w.transceivers[t].rtpSender.setTransport(w.transceivers[0].dtlsTransport),
                            w.transceivers[t].rtpReceiver && w.transceivers[t].rtpReceiver.setTransport(w.transceivers[0].dtlsTransport)),
                            "offer" !== T.type || i ? "answer" !== T.type || i || (u = (d = w.transceivers[t]).iceGatherer,
                            p = d.iceTransport,
                            l = d.dtlsTransport,
                            f = d.rtpReceiver,
                            m = d.sendEncodingParameters,
                            g = d.localCapabilities,
                            w.transceivers[t].recvEncodingParameters = h,
                            w.transceivers[t].remoteCapabilities = b,
                            w.transceivers[t].rtcpParameters = C,
                            (P || E) && k.length && p.setRemoteCandidates(k),
                            I && 0 !== t || (p.start(u, y, "controlling"),
                            l.start(S)),
                            w._transceive(d, "sendrecv" === a || "recvonly" === a, "sendrecv" === a || "sendonly" === a),
                            !f || "sendrecv" !== a && "sendonly" !== a ? delete d.rtpReceiver : (v = f.track,
                            s ? (L[s.stream] || (L[s.stream] = new x.MediaStream),
                            L[s.stream].addTrack(v),
                            R.push([v, f, L[s.stream]])) : (L.default || (L.default = new x.MediaStream),
                            L.default.addTrack(v),
                            R.push([v, f, L.default])))) : ((d = w.transceivers[t] || w._createTransceiver(r)).mid = c,
                            d.iceGatherer || (d.iceGatherer = I && 0 < t ? w.transceivers[0].iceGatherer : w._createIceGatherer(c, t)),
                            !E || I && 0 !== t || d.iceTransport.setRemoteCandidates(k),
                            g = x.RTCRtpReceiver.getCapabilities(r),
                            O < 15019 && (g.codecs = g.codecs.filter(function(e) {
                                return "rtx" !== e.name
                            })),
                            m = [{
                                ssrc: 1001 * (2 * t + 2)
                            }],
                            "sendrecv" !== a && "sendonly" !== a || (v = (f = new x.RTCRtpReceiver(d.dtlsTransport,r)).track,
                            s ? (L[s.stream] || (L[s.stream] = new x.MediaStream,
                            Object.defineProperty(L[s.stream], "id", {
                                get: function() {
                                    return s.stream
                                }
                            })),
                            Object.defineProperty(v, "id", {
                                get: function() {
                                    return s.track
                                }
                            }),
                            L[s.stream].addTrack(v),
                            R.push([v, f, L[s.stream]])) : (L.default || (L.default = new x.MediaStream),
                            L.default.addTrack(v),
                            R.push([v, f, L.default]))),
                            d.localCapabilities = g,
                            d.remoteCapabilities = b,
                            d.rtpReceiver = f,
                            d.rtcpParameters = C,
                            d.sendEncodingParameters = m,
                            d.recvEncodingParameters = h,
                            w._transceive(w.transceivers[t], !1, "sendrecv" === a || "sendonly" === a))
                        } else
                            w.transceivers[t] = {
                                mid: c,
                                isDatachannel: !0
                            }
                    }),
                    this.remoteDescription = {
                        type: T.type,
                        sdp: T.sdp
                    },
                    T.type) {
                    case "offer":
                        this._updateSignalingState("have-remote-offer");
                        break;
                    case "answer":
                        this._updateSignalingState("stable");
                        break;
                    default:
                        throw new TypeError('unsupported type "' + T.type + '"')
                    }
                    return Object.keys(L).forEach(function(e) {
                        var i = L[e];
                        if (i.getTracks().length) {
                            w.remoteStreams.push(i);
                            var t = new Event("addstream");
                            t.stream = i,
                            w.dispatchEvent(t),
                            null !== w.onaddstream && x.setTimeout(function() {
                                w.onaddstream(t)
                            }, 0),
                            R.forEach(function(e) {
                                var t = e[0]
                                  , n = e[1];
                                if (i.id === e[2].id) {
                                    var r = new Event("track");
                                    r.track = t,
                                    r.receiver = n,
                                    r.streams = [i],
                                    w.dispatchEvent(r),
                                    null !== w.ontrack && x.setTimeout(function() {
                                        w.ontrack(r)
                                    }, 0)
                                }
                            })
                        }
                    }),
                    x.setTimeout(function() {
                        w && w.transceivers && w.transceivers.forEach(function(e) {
                            e.iceTransport && "new" === e.iceTransport.state && 0 < e.iceTransport.getRemoteCandidates().length && (console.warn("Timeout for addRemoteCandidate. Consider sending an end-of-candidates notification"),
                            e.iceTransport.addRemoteCandidate({}))
                        })
                    }, 4e3),
                    1 < arguments.length && "function" == typeof arguments[1] && x.setTimeout(arguments[1], 0),
                    Promise.resolve()
                }
                ,
                e.prototype.close = function() {
                    this.transceivers.forEach(function(e) {
                        e.iceTransport && e.iceTransport.stop(),
                        e.dtlsTransport && e.dtlsTransport.stop(),
                        e.rtpSender && e.rtpSender.stop(),
                        e.rtpReceiver && e.rtpReceiver.stop()
                    }),
                    this._updateSignalingState("closed")
                }
                ,
                e.prototype._updateSignalingState = function(e) {
                    this.signalingState = e;
                    var t = new Event("signalingstatechange");
                    this.dispatchEvent(t),
                    null !== this.onsignalingstatechange && this.onsignalingstatechange(t)
                }
                ,
                e.prototype._maybeFireNegotiationNeeded = function() {
                    var t = this;
                    "stable" === this.signalingState && !0 !== this.needNegotiation && (this.needNegotiation = !0,
                    x.setTimeout(function() {
                        if (!1 !== t.needNegotiation) {
                            t.needNegotiation = !1;
                            var e = new Event("negotiationneeded");
                            t.dispatchEvent(e),
                            null !== t.onnegotiationneeded && t.onnegotiationneeded(e)
                        }
                    }, 0))
                }
                ,
                e.prototype._updateConnectionState = function() {
                    var e, t = {
                        new: 0,
                        closed: 0,
                        connecting: 0,
                        checking: 0,
                        connected: 0,
                        completed: 0,
                        disconnected: 0,
                        failed: 0
                    };
                    if (this.transceivers.forEach(function(e) {
                        t[e.iceTransport.state]++,
                        t[e.dtlsTransport.state]++
                    }),
                    t.connected += t.completed,
                    e = "new",
                    0 < t.failed ? e = "failed" : 0 < t.connecting || 0 < t.checking ? e = "connecting" : 0 < t.disconnected ? e = "disconnected" : 0 < t.new ? e = "new" : (0 < t.connected || 0 < t.completed) && (e = "connected"),
                    e !== this.iceConnectionState) {
                        this.iceConnectionState = e;
                        var n = new Event("iceconnectionstatechange");
                        this.dispatchEvent(n),
                        null !== this.oniceconnectionstatechange && this.oniceconnectionstatechange(n)
                    }
                }
                ,
                e.prototype.createOffer = function() {
                    var e, s = this;
                    if (this._pendingOffer)
                        throw new Error("createOffer called while there is a pending offer.");
                    1 === arguments.length && "function" != typeof arguments[0] ? e = arguments[0] : 3 === arguments.length && (e = arguments[2]);
                    var t = this.transceivers.filter(function(e) {
                        return "audio" === e.kind
                    }).length
                      , n = this.transceivers.filter(function(e) {
                        return "video" === e.kind
                    }).length;
                    if (e) {
                        if (e.mandatory || e.optional)
                            throw new TypeError("Legacy mandatory/optional constraints not supported.");
                        void 0 !== e.offerToReceiveAudio && (t = !0 === e.offerToReceiveAudio ? 1 : !1 === e.offerToReceiveAudio ? 0 : e.offerToReceiveAudio),
                        void 0 !== e.offerToReceiveVideo && (n = !0 === e.offerToReceiveVideo ? 1 : !1 === e.offerToReceiveVideo ? 0 : e.offerToReceiveVideo)
                    }
                    for (this.transceivers.forEach(function(e) {
                        "audio" === e.kind ? --t < 0 && (e.wantReceive = !1) : "video" === e.kind && --n < 0 && (e.wantReceive = !1)
                    }); 0 < t || 0 < n; )
                        0 < t && (this._createTransceiver("audio"),
                        t--),
                        0 < n && (this._createTransceiver("video"),
                        n--);
                    var c = function(e) {
                        var t = e.filter(function(e) {
                            return "audio" === e.kind
                        })
                          , n = e.filter(function(e) {
                            return "video" === e.kind
                        });
                        for (e = []; t.length || n.length; )
                            t.length && e.push(t.shift()),
                            n.length && e.push(n.shift());
                        return e
                    }(this.transceivers)
                      , r = A.writeSessionBoilerplate();
                    c.forEach(function(e, t) {
                        var n = e.track
                          , r = e.kind
                          , i = A.generateIdentifier();
                        e.mid = i,
                        e.iceGatherer || (e.iceGatherer = s.usingBundle && 0 < t ? c[0].iceGatherer : s._createIceGatherer(i, t));
                        var o = x.RTCRtpSender.getCapabilities(r);
                        O < 15019 && (o.codecs = o.codecs.filter(function(e) {
                            return "rtx" !== e.name
                        })),
                        o.codecs.forEach(function(e) {
                            "H264" === e.name && void 0 === e.parameters["level-asymmetry-allowed"] && (e.parameters["level-asymmetry-allowed"] = "1")
                        });
                        var a = [{
                            ssrc: 1001 * (2 * t + 1)
                        }];
                        n && 15019 <= O && "video" === r && (a[0].rtx = {
                            ssrc: 1001 * (2 * t + 1) + 1
                        }),
                        e.wantReceive && (e.rtpReceiver = new x.RTCRtpReceiver(e.dtlsTransport,r)),
                        e.localCapabilities = o,
                        e.sendEncodingParameters = a
                    }),
                    "max-compat" !== this._config.bundlePolicy && (r += "a=group:BUNDLE " + c.map(function(e) {
                        return e.mid
                    }).join(" ") + "\r\n"),
                    r += "a=ice-options:trickle\r\n",
                    c.forEach(function(e, t) {
                        r += A.writeMediaSection(e, e.localCapabilities, "offer", e.stream),
                        r += "a=rtcp-rsize\r\n"
                    }),
                    this._pendingOffer = c;
                    var i = new x.RTCSessionDescription({
                        type: "offer",
                        sdp: r
                    });
                    return arguments.length && "function" == typeof arguments[0] && x.setTimeout(arguments[0], 0, i),
                    Promise.resolve(i)
                }
                ,
                e.prototype.createAnswer = function() {
                    var i = A.writeSessionBoilerplate();
                    this.usingBundle && (i += "a=group:BUNDLE " + this.transceivers.map(function(e) {
                        return e.mid
                    }).join(" ") + "\r\n"),
                    this.transceivers.forEach(function(e, t) {
                        if (e.isDatachannel)
                            i += "m=application 0 DTLS/SCTP 5000\r\nc=IN IP4 0.0.0.0\r\na=mid:" + e.mid + "\r\n";
                        else {
                            var n;
                            if (e.stream)
                                "audio" === e.kind ? n = e.stream.getAudioTracks()[0] : "video" === e.kind && (n = e.stream.getVideoTracks()[0]),
                                n && 15019 <= O && "video" === e.kind && (e.sendEncodingParameters[0].rtx = {
                                    ssrc: 1001 * (2 * t + 2) + 1
                                });
                            var r = m(e.localCapabilities, e.remoteCapabilities);
                            !r.codecs.filter(function(e) {
                                return "rtx" === e.name.toLowerCase()
                            }).length && e.sendEncodingParameters[0].rtx && delete e.sendEncodingParameters[0].rtx,
                            i += A.writeMediaSection(e, r, "answer", e.stream),
                            e.rtcpParameters && e.rtcpParameters.reducedSize && (i += "a=rtcp-rsize\r\n")
                        }
                    });
                    var e = new x.RTCSessionDescription({
                        type: "answer",
                        sdp: i
                    });
                    return arguments.length && "function" == typeof arguments[0] && x.setTimeout(arguments[0], 0, e),
                    Promise.resolve(e)
                }
                ,
                e.prototype.addIceCandidate = function(e) {
                    if (e) {
                        var t = e.sdpMLineIndex;
                        if (e.sdpMid)
                            for (var n = 0; n < this.transceivers.length; n++)
                                if (this.transceivers[n].mid === e.sdpMid) {
                                    t = n;
                                    break
                                }
                        var r = this.transceivers[t];
                        if (r) {
                            var i = 0 < Object.keys(e.candidate).length ? A.parseCandidate(e.candidate) : {};
                            if ("tcp" === i.protocol && (0 === i.port || 9 === i.port))
                                return Promise.resolve();
                            if (i.component && "1" !== i.component && 1 !== i.component)
                                return Promise.resolve();
                            r.iceTransport.addRemoteCandidate(i);
                            var o = A.splitSections(this.remoteDescription.sdp);
                            o[t + 1] += (i.type ? e.candidate.trim() : "a=end-of-candidates") + "\r\n",
                            this.remoteDescription.sdp = o.join("")
                        }
                    } else
                        for (var a = 0; a < this.transceivers.length; a++)
                            if (this.transceivers[a].iceTransport.addRemoteCandidate({}),
                            this.usingBundle)
                                return Promise.resolve();
                    return 1 < arguments.length && "function" == typeof arguments[1] && x.setTimeout(arguments[1], 0),
                    Promise.resolve()
                }
                ,
                e.prototype.getStats = function() {
                    var n = [];
                    this.transceivers.forEach(function(t) {
                        ["rtpSender", "rtpReceiver", "iceGatherer", "iceTransport", "dtlsTransport"].forEach(function(e) {
                            t[e] && n.push(t[e].getStats())
                        })
                    });
                    var i = 1 < arguments.length && "function" == typeof arguments[1] && arguments[1];
                    return new Promise(function(t) {
                        var r = new Map;
                        Promise.all(n).then(function(e) {
                            e.forEach(function(n) {
                                Object.keys(n).forEach(function(e) {
                                    var t;
                                    n[e].type = {
                                        inboundrtp: "inbound-rtp",
                                        outboundrtp: "outbound-rtp",
                                        candidatepair: "candidate-pair",
                                        localcandidate: "local-candidate",
                                        remotecandidate: "remote-candidate"
                                    }[(t = n[e]).type] || t.type,
                                    r.set(e, n[e])
                                })
                            }),
                            i && x.setTimeout(i, 0, r),
                            t(r)
                        })
                    }
                    )
                }
                ,
                e
            }
        }
        , {
            sdp: 1
        }],
        9: [function(e, t, n) {
            "use strict";
            var r = e("../utils")
              , i = {
                shimOnTrack: function(e) {
                    "object" != typeof e || !e.RTCPeerConnection || "ontrack"in e.RTCPeerConnection.prototype || Object.defineProperty(e.RTCPeerConnection.prototype, "ontrack", {
                        get: function() {
                            return this._ontrack
                        },
                        set: function(e) {
                            this._ontrack && (this.removeEventListener("track", this._ontrack),
                            this.removeEventListener("addstream", this._ontrackpoly)),
                            this.addEventListener("track", this._ontrack = e),
                            this.addEventListener("addstream", this._ontrackpoly = function(n) {
                                n.stream.getTracks().forEach(function(e) {
                                    var t = new Event("track");
                                    t.track = e,
                                    t.receiver = {
                                        track: e
                                    },
                                    t.streams = [n.stream],
                                    this.dispatchEvent(t)
                                }
                                .bind(this))
                            }
                            .bind(this))
                        }
                    })
                },
                shimSourceObject: function(e) {
                    "object" == typeof e && (!e.HTMLMediaElement || "srcObject"in e.HTMLMediaElement.prototype || Object.defineProperty(e.HTMLMediaElement.prototype, "srcObject", {
                        get: function() {
                            return this.mozSrcObject
                        },
                        set: function(e) {
                            this.mozSrcObject = e
                        }
                    }))
                },
                shimPeerConnection: function(s) {
                    var c = r.detectBrowser(s);
                    if ("object" == typeof s && (s.RTCPeerConnection || s.mozRTCPeerConnection)) {
                        s.RTCPeerConnection || (s.RTCPeerConnection = function(e, t) {
                            if (c.version < 38 && e && e.iceServers) {
                                for (var n = [], r = 0; r < e.iceServers.length; r++) {
                                    var i = e.iceServers[r];
                                    if (i.hasOwnProperty("urls"))
                                        for (var o = 0; o < i.urls.length; o++) {
                                            var a = {
                                                url: i.urls[o]
                                            };
                                            0 === i.urls[o].indexOf("turn") && (a.username = i.username,
                                            a.credential = i.credential),
                                            n.push(a)
                                        }
                                    else
                                        n.push(e.iceServers[r])
                                }
                                e.iceServers = n
                            }
                            return new s.mozRTCPeerConnection(e,t)
                        }
                        ,
                        s.RTCPeerConnection.prototype = s.mozRTCPeerConnection.prototype,
                        s.mozRTCPeerConnection.generateCertificate && Object.defineProperty(s.RTCPeerConnection, "generateCertificate", {
                            get: function() {
                                return s.mozRTCPeerConnection.generateCertificate
                            }
                        }),
                        s.RTCSessionDescription = s.mozRTCSessionDescription,
                        s.RTCIceCandidate = s.mozRTCIceCandidate),
                        ["setLocalDescription", "setRemoteDescription", "addIceCandidate"].forEach(function(e) {
                            var t = s.RTCPeerConnection.prototype[e];
                            s.RTCPeerConnection.prototype[e] = function() {
                                return arguments[0] = new ("addIceCandidate" === e ? s.RTCIceCandidate : s.RTCSessionDescription)(arguments[0]),
                                t.apply(this, arguments)
                            }
                        });
                        var e = s.RTCPeerConnection.prototype.addIceCandidate;
                        s.RTCPeerConnection.prototype.addIceCandidate = function() {
                            return arguments[0] ? e.apply(this, arguments) : (arguments[1] && arguments[1].apply(null),
                            Promise.resolve())
                        }
                        ;
                        var o = {
                            inboundrtp: "inbound-rtp",
                            outboundrtp: "outbound-rtp",
                            candidatepair: "candidate-pair",
                            localcandidate: "local-candidate",
                            remotecandidate: "remote-candidate"
                        }
                          , n = s.RTCPeerConnection.prototype.getStats;
                        s.RTCPeerConnection.prototype.getStats = function(e, i, t) {
                            return n.apply(this, [e || null]).then(function(n) {
                                var t, r;
                                if (c.version < 48 && (t = n,
                                r = new Map,
                                Object.keys(t).forEach(function(e) {
                                    r.set(e, t[e]),
                                    r[e] = t[e]
                                }),
                                n = r),
                                c.version < 53 && !i)
                                    try {
                                        n.forEach(function(e) {
                                            e.type = o[e.type] || e.type
                                        })
                                    } catch (e) {
                                        if ("TypeError" !== e.name)
                                            throw e;
                                        n.forEach(function(e, t) {
                                            n.set(t, Object.assign({}, e, {
                                                type: o[e.type] || e.type
                                            }))
                                        })
                                    }
                                return n
                            }).then(i, t)
                        }
                    }
                }
            };
            t.exports = {
                shimOnTrack: i.shimOnTrack,
                shimSourceObject: i.shimSourceObject,
                shimPeerConnection: i.shimPeerConnection,
                shimGetUserMedia: e("./getusermedia")
            }
        }
        , {
            "../utils": 12,
            "./getusermedia": 10
        }],
        10: [function(e, t, n) {
            "use strict";
            var l = e("../utils")
              , f = l.log;
            t.exports = function(e) {
                var i = l.detectBrowser(e)
                  , o = e && e.navigator
                  , t = e && e.MediaStreamTrack
                  , a = function(e) {
                    return {
                        name: {
                            InternalError: "NotReadableError",
                            NotSupportedError: "TypeError",
                            PermissionDeniedError: "NotAllowedError",
                            SecurityError: "NotAllowedError"
                        }[e.name] || e.name,
                        message: {
                            "The operation is insecure.": "The request is not allowed by the user agent or the platform in the current context."
                        }[e.message] || e.message,
                        constraint: e.constraint,
                        toString: function() {
                            return this.name + (this.message && ": ") + this.message
                        }
                    }
                }
                  , r = function(e, t, n) {
                    var r = function(r) {
                        if ("object" != typeof r || r.require)
                            return r;
                        var i = [];
                        return Object.keys(r).forEach(function(e) {
                            if ("require" !== e && "advanced" !== e && "mediaSource" !== e) {
                                var t = r[e] = "object" == typeof r[e] ? r[e] : {
                                    ideal: r[e]
                                };
                                if (void 0 === t.min && void 0 === t.max && void 0 === t.exact || i.push(e),
                                void 0 !== t.exact && ("number" == typeof t.exact ? t.min = t.max = t.exact : r[e] = t.exact,
                                delete t.exact),
                                void 0 !== t.ideal) {
                                    r.advanced = r.advanced || [];
                                    var n = {};
                                    "number" == typeof t.ideal ? n[e] = {
                                        min: t.ideal,
                                        max: t.ideal
                                    } : n[e] = t.ideal,
                                    r.advanced.push(n),
                                    delete t.ideal,
                                    Object.keys(t).length || delete r[e]
                                }
                            }
                        }),
                        i.length && (r.require = i),
                        r
                    };
                    return e = JSON.parse(JSON.stringify(e)),
                    i.version < 38 && (f("spec: " + JSON.stringify(e)),
                    e.audio && (e.audio = r(e.audio)),
                    e.video && (e.video = r(e.video)),
                    f("ff37: " + JSON.stringify(e))),
                    o.mozGetUserMedia(e, t, function(e) {
                        n(a(e))
                    })
                };
                if (o.mediaDevices || (o.mediaDevices = {
                    getUserMedia: function(n) {
                        return new Promise(function(e, t) {
                            r(n, e, t)
                        }
                        )
                    },
                    addEventListener: function() {},
                    removeEventListener: function() {}
                }),
                o.mediaDevices.enumerateDevices = o.mediaDevices.enumerateDevices || function() {
                    return new Promise(function(e) {
                        e([{
                            kind: "audioinput",
                            deviceId: "default",
                            label: "",
                            groupId: ""
                        }, {
                            kind: "videoinput",
                            deviceId: "default",
                            label: "",
                            groupId: ""
                        }])
                    }
                    )
                }
                ,
                i.version < 41) {
                    var n = o.mediaDevices.enumerateDevices.bind(o.mediaDevices);
                    o.mediaDevices.enumerateDevices = function() {
                        return n().then(void 0, function(e) {
                            if ("NotFoundError" === e.name)
                                return [];
                            throw e
                        })
                    }
                }
                if (i.version < 49) {
                    var s = o.mediaDevices.getUserMedia.bind(o.mediaDevices);
                    o.mediaDevices.getUserMedia = function(t) {
                        return s(t).then(function(e) {
                            if (t.audio && !e.getAudioTracks().length || t.video && !e.getVideoTracks().length)
                                throw e.getTracks().forEach(function(e) {
                                    e.stop()
                                }),
                                new DOMException("The object can not be found here.","NotFoundError");
                            return e
                        }, function(e) {
                            return Promise.reject(a(e))
                        })
                    }
                }
                if (!(55 < i.version && "autoGainControl"in o.mediaDevices.getSupportedConstraints())) {
                    var c = function(e, t, n) {
                        t in e && !(n in e) && (e[n] = e[t],
                        delete e[t])
                    }
                      , d = o.mediaDevices.getUserMedia.bind(o.mediaDevices);
                    if (o.mediaDevices.getUserMedia = function(e) {
                        return "object" == typeof e && "object" == typeof e.audio && (e = JSON.parse(JSON.stringify(e)),
                        c(e.audio, "autoGainControl", "mozAutoGainControl"),
                        c(e.audio, "noiseSuppression", "mozNoiseSuppression")),
                        d(e)
                    }
                    ,
                    t && t.prototype.getSettings) {
                        var u = t.prototype.getSettings;
                        t.prototype.getSettings = function() {
                            var e = u.apply(this, arguments);
                            return c(e, "mozAutoGainControl", "autoGainControl"),
                            c(e, "mozNoiseSuppression", "noiseSuppression"),
                            e
                        }
                    }
                    if (t && t.prototype.applyConstraints) {
                        var p = t.prototype.applyConstraints;
                        t.prototype.applyConstraints = function(e) {
                            return "audio" === this.kind && "object" == typeof e && (e = JSON.parse(JSON.stringify(e)),
                            c(e, "autoGainControl", "mozAutoGainControl"),
                            c(e, "noiseSuppression", "mozNoiseSuppression")),
                            p.apply(this, [e])
                        }
                    }
                }
                o.getUserMedia = function(e, t, n) {
                    if (i.version < 44)
                        return r(e, t, n);
                    console.warn("navigator.getUserMedia has been replaced by navigator.mediaDevices.getUserMedia"),
                    o.mediaDevices.getUserMedia(e).then(t, n)
                }
            }
        }
        , {
            "../utils": 12
        }],
        11: [function(e, t, n) {
            "use strict";
            var r = {
                shimLocalStreamsAPI: function(e) {
                    if ("object" == typeof e && e.RTCPeerConnection) {
                        if ("getLocalStreams"in e.RTCPeerConnection.prototype || (e.RTCPeerConnection.prototype.getLocalStreams = function() {
                            return this._localStreams || (this._localStreams = []),
                            this._localStreams
                        }
                        ),
                        "getStreamById"in e.RTCPeerConnection.prototype || (e.RTCPeerConnection.prototype.getStreamById = function(t) {
                            var n = null;
                            return this._localStreams && this._localStreams.forEach(function(e) {
                                e.id === t && (n = e)
                            }),
                            this._remoteStreams && this._remoteStreams.forEach(function(e) {
                                e.id === t && (n = e)
                            }),
                            n
                        }
                        ),
                        !("addStream"in e.RTCPeerConnection.prototype)) {
                            var r = e.RTCPeerConnection.prototype.addTrack;
                            e.RTCPeerConnection.prototype.addStream = function(t) {
                                this._localStreams || (this._localStreams = []),
                                -1 === this._localStreams.indexOf(t) && this._localStreams.push(t);
                                var n = this;
                                t.getTracks().forEach(function(e) {
                                    r.call(n, e, t)
                                })
                            }
                            ,
                            e.RTCPeerConnection.prototype.addTrack = function(e, t) {
                                t && (this._localStreams ? -1 === this._localStreams.indexOf(t) && this._localStreams.push(t) : this._localStreams = [t]),
                                r.call(this, e, t)
                            }
                        }
                        "removeStream"in e.RTCPeerConnection.prototype || (e.RTCPeerConnection.prototype.removeStream = function(e) {
                            this._localStreams || (this._localStreams = []);
                            var t = this._localStreams.indexOf(e);
                            if (-1 !== t) {
                                this._localStreams.splice(t, 1);
                                var n = this
                                  , r = e.getTracks();
                                this.getSenders().forEach(function(e) {
                                    -1 !== r.indexOf(e.track) && n.removeTrack(e)
                                })
                            }
                        }
                        )
                    }
                },
                shimRemoteStreamsAPI: function(e) {
                    "object" == typeof e && e.RTCPeerConnection && ("getRemoteStreams"in e.RTCPeerConnection.prototype || (e.RTCPeerConnection.prototype.getRemoteStreams = function() {
                        return this._remoteStreams ? this._remoteStreams : []
                    }
                    ),
                    "onaddstream"in e.RTCPeerConnection.prototype || Object.defineProperty(e.RTCPeerConnection.prototype, "onaddstream", {
                        get: function() {
                            return this._onaddstream
                        },
                        set: function(e) {
                            this._onaddstream && (this.removeEventListener("addstream", this._onaddstream),
                            this.removeEventListener("track", this._onaddstreampoly)),
                            this.addEventListener("addstream", this._onaddstream = e),
                            this.addEventListener("track", this._onaddstreampoly = function(e) {
                                var t = e.streams[0];
                                if (this._remoteStreams || (this._remoteStreams = []),
                                !(0 <= this._remoteStreams.indexOf(t))) {
                                    this._remoteStreams.push(t);
                                    var n = new Event("addstream");
                                    n.stream = e.streams[0],
                                    this.dispatchEvent(n)
                                }
                            }
                            .bind(this))
                        }
                    }))
                },
                shimCallbacksAPI: function(e) {
                    if ("object" == typeof e && e.RTCPeerConnection) {
                        var t = e.RTCPeerConnection.prototype
                          , i = t.createOffer
                          , o = t.createAnswer
                          , a = t.setLocalDescription
                          , s = t.setRemoteDescription
                          , c = t.addIceCandidate;
                        t.createOffer = function(e, t) {
                            var n = 2 <= arguments.length ? arguments[2] : e
                              , r = i.apply(this, [n]);
                            return t ? (r.then(e, t),
                            Promise.resolve()) : r
                        }
                        ,
                        t.createAnswer = function(e, t) {
                            var n = 2 <= arguments.length ? arguments[2] : e
                              , r = o.apply(this, [n]);
                            return t ? (r.then(e, t),
                            Promise.resolve()) : r
                        }
                        ;
                        var n = function(e, t, n) {
                            var r = a.apply(this, [e]);
                            return n ? (r.then(t, n),
                            Promise.resolve()) : r
                        };
                        t.setLocalDescription = n,
                        n = function(e, t, n) {
                            var r = s.apply(this, [e]);
                            return n ? (r.then(t, n),
                            Promise.resolve()) : r
                        }
                        ,
                        t.setRemoteDescription = n,
                        n = function(e, t, n) {
                            var r = c.apply(this, [e]);
                            return n ? (r.then(t, n),
                            Promise.resolve()) : r
                        }
                        ,
                        t.addIceCandidate = n
                    }
                },
                shimGetUserMedia: function(e) {
                    var r = e && e.navigator;
                    r.getUserMedia || (r.webkitGetUserMedia ? r.getUserMedia = r.webkitGetUserMedia.bind(r) : r.mediaDevices && r.mediaDevices.getUserMedia && (r.getUserMedia = function(e, t, n) {
                        r.mediaDevices.getUserMedia(e).then(t, n)
                    }
                    .bind(r)))
                }
            };
            t.exports = {
                shimCallbacksAPI: r.shimCallbacksAPI,
                shimLocalStreamsAPI: r.shimLocalStreamsAPI,
                shimRemoteStreamsAPI: r.shimRemoteStreamsAPI,
                shimGetUserMedia: r.shimGetUserMedia
            }
        }
        , {}],
        12: [function(e, t, n) {
            "use strict";
            var r = !0
              , i = {
                disableLog: function(e) {
                    return "boolean" != typeof e ? new Error("Argument type: " + typeof e + ". Please use a boolean.") : (r = e) ? "adapter.js logging disabled" : "adapter.js logging enabled"
                },
                log: function() {
                    if ("object" == typeof window) {
                        if (r)
                            return;
                        "undefined" != typeof console && "function" == typeof console.log && console.log.apply(console, arguments)
                    }
                },
                extractVersion: function(e, t, n) {
                    var r = e.match(t);
                    return r && r.length >= n && parseInt(r[n], 10)
                },
                detectBrowser: function(e) {
                    var t = e && e.navigator
                      , n = {
                        browser: null,
                        version: null
                    };
                    if (void 0 === e || !e.navigator)
                        return n.browser = "Not a browser.",
                        n;
                    if (t.mozGetUserMedia)
                        n.browser = "firefox",
                        n.version = this.extractVersion(t.userAgent, /Firefox\/(\d+)\./, 1);
                    else if (t.webkitGetUserMedia)
                        if (e.webkitRTCPeerConnection)
                            n.browser = "chrome",
                            n.version = this.extractVersion(t.userAgent, /Chrom(e|ium)\/(\d+)\./, 2);
                        else {
                            if (!t.userAgent.match(/Version\/(\d+).(\d+)/))
                                return n.browser = "Unsupported webkit-based browser with GUM support but no WebRTC support.",
                                n;
                            n.browser = "safari",
                            n.version = this.extractVersion(t.userAgent, /AppleWebKit\/(\d+)\./, 1)
                        }
                    else if (t.mediaDevices && t.userAgent.match(/Edge\/(\d+).(\d+)$/))
                        n.browser = "edge",
                        n.version = this.extractVersion(t.userAgent, /Edge\/(\d+).(\d+)$/, 2);
                    else {
                        if (!t.mediaDevices || !t.userAgent.match(/AppleWebKit\/(\d+)\./))
                            return n.browser = "Not a supported browser.",
                            n;
                        n.browser = "safari",
                        n.version = this.extractVersion(t.userAgent, /AppleWebKit\/(\d+)\./, 1)
                    }
                    return n
                },
                shimCreateObjectURL: function(e) {
                    var t = e && e.URL;
                    if ("object" == typeof e && e.HTMLMediaElement && "srcObject"in e.HTMLMediaElement.prototype) {
                        var n = t.createObjectURL.bind(t)
                          , r = t.revokeObjectURL.bind(t)
                          , i = new Map
                          , o = 0;
                        t.createObjectURL = function(e) {
                            if ("getTracks"in e) {
                                var t = "polyblob:" + ++o;
                                return i.set(t, e),
                                console.log("URL.createObjectURL(stream) is deprecated! Use elem.srcObject = stream instead!"),
                                t
                            }
                            return n(e)
                        }
                        ,
                        t.revokeObjectURL = function(e) {
                            r(e),
                            i.delete(e)
                        }
                        ;
                        var a = Object.getOwnPropertyDescriptor(e.HTMLMediaElement.prototype, "src");
                        Object.defineProperty(e.HTMLMediaElement.prototype, "src", {
                            get: function() {
                                return a.get.apply(this)
                            },
                            set: function(e) {
                                return this.srcObject = i.get(e) || null,
                                a.set.apply(this, [e])
                            }
                        });
                        var s = e.HTMLMediaElement.prototype.setAttribute;
                        e.HTMLMediaElement.prototype.setAttribute = function() {
                            return 2 === arguments.length && "src" === ("" + arguments[0]).toLowerCase() && (this.srcObject = i.get(arguments[1]) || null),
                            s.apply(this, arguments)
                        }
                    }
                }
            };
            t.exports = {
                log: i.log,
                disableLog: i.disableLog,
                extractVersion: i.extractVersion,
                shimCreateObjectURL: i.shimCreateObjectURL,
                detectBrowser: i.detectBrowser.bind(i)
            }
        }
        , {}]
    }, {}, [2])(2)
});
var SERVICE_TYPE_PING = 1
  , SERVICE_TYPE_AUTHEN = 2
  , SERVICE_TYPE_CALLOUT_START_CALL = 3
  , SERVICE_TYPE_CALLOUT_DATA = 4
  , SERVICE_TYPE_CALLOUT_STOP_CALL = 5
  , SERVICE_TYPE_CALLOUT_STATUS_CHANGE = 6
  , SERVICE_TYPE_CALLOUT_DATA_FROM_GATEWAY = 7
  , SERVICE_TYPE_VIDEO_CONFERENCE_MAKE_ROOM = 8
  , SERVICE_TYPE_VIDEO_CONFERENCE_JOIN_ROOM = 9
  , SERVICE_TYPE_VIDEO_CONFERENCE_JOIN_ROOM_NOTIFICATION = 10
  , SERVICE_TYPE_VIDEO_CONFERENCE_LEAVE_ROOM_NOTIFICATION = 11
  , SERVICE_TYPE_VIDEO_CONFERENCE_LEAVE_ROOM = 12
  , SERVICE_TYPE_VIDEO_CONFERENCE_STREAM_ADDED = 15
  , SERVICE_TYPE_VIDEO_CONFERENCE_STREAM_REMOVED = 16
  , SERVICE_TYPE_CALLIN_SDP_CANDIDATE = 19
  , SERVICE_TYPE_CALLIN_STATUS_CHANGE = 20
  , SERVICE_TYPE_CALLIN_STOP_CALL = 21
  , SERVICE_TYPE_CALLIN_SDP_CANDIDATE_FROM_SERVER = 22
  , SERVICE_TYPE_CALLIN_STOP_CALL_FROM_SERVER = 25
  , SERVICE_TYPE_CALL_START = 26
  , SERVICE_TYPE_CALL_SDP_CANDIDATE = 27
  , SERVICE_TYPE_CALL_STOP = 28
  , SERVICE_TYPE_CALL_SDP_CANDIDATE_FROM_SERVER = 29
  , SERVICE_TYPE_CALL_STOP_FROM_SERVER = 30
  , SERVICE_TYPE_CALL_STATE = 31
  , SERVICE_TYPE_CALL_STATE_FROM_SERVER = 32
  , SERVICE_TYPE_CALL_START_FROM_SERVER = 33
  , SERVICE_TYPE_CALL_DTMF = 34
  , SERVICE_TYPE_CALL_DTMF_FROM_SERVER = 35
  , SERVICE_TYPE_CUSTOM_MESSAGE = 54
  , SERVICE_TYPE_CUSTOM_MESSAGE_FROM_SERVER = 55
  , Stringee = Stringee || {
    fmSignalingServerUrl: "https://v1.stringee.com:6888/",
    socket: null,
    socketManager: null,
    autoReconnect: !0,
    requestId: 1,
    callbacks: new HashMap,
    userInfo: {}
};
Stringee.pushCallback = function(e, t) {
    if (t) {
        var n = this.callbacks.get(e);
        n || (n = []),
        n.push(t),
        this.callbacks.put(e, n)
    }
}
,
Stringee.callCallback = function(e, t) {
    var n = !1
      , r = this.callbacks.get(e);
    if (r) {
        var i = r.pop();
        i && (i(t),
        n = !0)
    }
    return n
}
,
Stringee.connect = function(n, r) {
    Stringee.socket = io.connect(this.fmSignalingServerUrl, {
        reconnection: !0
    }),
    Stringee.socket.on("connect", function(e) {
        var t = {
            accesstoken: n
        };
        console.log("Connected, send login packet"),
        Stringee.sendMessage(SERVICE_TYPE_AUTHEN, t, r)
    }),
    Stringee.socket.on("disconnect", function() {}),
    Stringee.socket.on("connect_error", function(e) {}),
    Stringee.socket.on("reconnect", function() {}),
    Stringee.socket.on("EventPacket", function(e) {
        var t = JSON.parse(e.body);
        Stringee.packetReceived(e.service, t)
    })
}
,
Stringee.sendMessage = function(e, t, n) {
    n && Stringee.pushCallback("packet_" + e + "_" + Stringee.requestId, n),
    t.requestId = Stringee.requestId;
    var r = {
        service: e,
        body: JSON.stringify(t)
    };
    this.socket.emit("EventPacket", r),
    Stringee.requestId++
}
,
Stringee.packetReceived = function(e, t) {
    var n = "packet_" + e + "_" + t.requestId
      , r = Stringee.callCallback(n, t);
    e === SERVICE_TYPE_CALLOUT_STATUS_CHANGE ? Stringee.calloutChangeStatusProcessor(e, t) : e === SERVICE_TYPE_AUTHEN ? Stringee.authenProcessor(e, t) : e === SERVICE_TYPE_CALLOUT_DATA_FROM_GATEWAY ? Stringee.calloutDataFromServerProcessor(e, t) : e === SERVICE_TYPE_VIDEO_CONFERENCE_JOIN_ROOM_NOTIFICATION ? Stringee.joinRoomNotificationProcessor(e, t) : e === SERVICE_TYPE_VIDEO_CONFERENCE_LEAVE_ROOM_NOTIFICATION ? Stringee.leaveRoomNotificationProcessor(e, t) : e === SERVICE_TYPE_CALLIN_SDP_CANDIDATE_FROM_SERVER ? Stringee.callinSdpFromServerProcessor(e, t) : e === SERVICE_TYPE_CALL_SDP_CANDIDATE_FROM_SERVER ? Stringee.callSdpCandidateFromServerProcessor(e, t) : e === SERVICE_TYPE_CALL_STATE_FROM_SERVER ? Stringee.callStateFromServerProcessor(e, t) : e === SERVICE_TYPE_CALL_STOP_FROM_SERVER ? Stringee.callStopFromServerProcessor(e, t) : e === SERVICE_TYPE_CALL_START_FROM_SERVER ? Stringee.callStartFromServerProcessor(e, t) : e === SERVICE_TYPE_CUSTOM_MESSAGE_FROM_SERVER ? Stringee.onCustomMessage(t) : e === SERVICE_TYPE_PING ? (console.log("SERVICE_TYPE_PING received"),
    Stringee.sendMessage(SERVICE_TYPE_PING, {}, null)) : r || console.log("ERROR", "Could not found processor for: service=" + e + "; bodyJson=" + JSON.stringify(t))
}
,
Stringee.sendCustomMessage = function(e, t, n) {
    var r = {
        toUser: e,
        message: t
    };
    Stringee.sendMessage(SERVICE_TYPE_CUSTOM_MESSAGE, r, n)
}
,
Stringee.onCustomMessage = function(e) {
    console.log("onCustomMessage 111: " + JSON.stringify(e))
}
,
Stringee.authenProcessor = function(e, t) {
    0 == t.r ? Stringee.userInfo = {
        connectionId: t.connectionId,
        userId: t.userId
    } : 6 == t.r && Stringee.onRequestNewAccessToken()
}
,
Stringee.onRequestNewAccessToken = function() {
    console.log("Please implement onRequestNewAccessToken method")
}
,
Stringee.setNewAccessToken = function(e) {
    Stringee.connect(e, null)
}
;
var WEBRTC_ERROR_CODE_CREATE_PEER_CONNECTION_FAILED = 1001
  , WEBRTC_ERROR_CODE_GET_USER_MEDIA_ERROR = 1002
  , WEBRTC_ERROR_CODE_CREATE_SDP_FAILED = 1003
  , FmVideoPrivate = FmVideoPrivate || {};
FmVideoPrivate.EventDispatcher = function(r) {
    var e = {};
    return r.dispatcher = {},
    r.dispatcher.eventListeners = {},
    e.addEventListener = function(e, t) {
        void 0 === r.dispatcher.eventListeners[e] && (r.dispatcher.eventListeners[e] = []),
        r.dispatcher.eventListeners[e].push(t)
    }
    ,
    e.removeEventListener = function(e, t) {
        var n;
        -1 !== (n = r.dispatcher.eventListeners[e].indexOf(t)) && r.dispatcher.eventListeners[e].splice(n, 1)
    }
    ,
    e.dispatchEvent = function(e) {
        var t;
        for (t in L.Logger.debug("Event: " + e.type),
        r.dispatcher.eventListeners[e.type])
            r.dispatcher.eventListeners[e.type].hasOwnProperty(t) && r.dispatcher.eventListeners[e.type][t](e)
    }
    ,
    e
}
,
FmVideoPrivate.LicodeEvent = function(e) {
    var t = {};
    return t.type = e.type,
    t
}
,
FmVideoPrivate.RoomEvent = function(e) {
    var t = FmVideoPrivate.LicodeEvent(e);
    return t.streams = e.streams,
    t.message = e.message,
    t
}
,
FmVideoPrivate.StreamEvent = function(e) {
    var t = FmVideoPrivate.LicodeEvent(e);
    return t.stream = e.stream,
    t.msg = e.msg,
    t.bandwidth = e.bandwidth,
    t
}
,
FmVideoPrivate.PublisherEvent = function(e) {
    return FmVideoPrivate.LicodeEvent(e)
}
,
FmVideoPrivate = FmVideoPrivate || {},
FmVideoPrivate.FcStack = function(t) {
    var n = {
        pc_config: {},
        peerConnection: {},
        desc: {},
        signalCallback: void 0,
        close: function() {
            console.log("Close FcStack")
        },
        createOffer: function() {
            console.log("FCSTACK: CreateOffer")
        },
        addStream: function(e) {
            console.log("FCSTACK: addStream", e)
        },
        processSignalingMessage: function(e) {
            console.log("FCSTACK: processSignaling", e),
            void 0 !== n.signalCallback && n.signalCallback(e)
        },
        sendSignalingMessage: function(e) {
            console.log("FCSTACK: Sending signaling Message", e),
            t.callback(e)
        },
        setSignalingCallback: function(e) {
            console.log("FCSTACK: Setting signalling callback"),
            n.signalCallback = e
        }
    };
    return n
}
,
FmVideoPrivate = FmVideoPrivate || {},
FmVideoPrivate.BowserStack = function(r) {
    var i = {}
      , e = webkitRTCPeerConnection;
    i.pc_config = {
        iceServers: []
    },
    i.con = {
        optional: [{
            DtlsSrtpKeyAgreement: !0
        }]
    },
    void 0 !== r.stunServerUrl && i.pc_config.iceServers.push({
        url: r.stunServerUrl
    }),
    (r.turnServer || {}).url && i.pc_config.iceServers.push({
        username: r.turnServer.username,
        credential: r.turnServer.password,
        url: r.turnServer.url
    }),
    void 0 === r.audio && (r.audio = !0),
    void 0 === r.video && (r.video = !0),
    i.mediaConstraints = {
        offerToReceiveVideo: r.video,
        offerToReceiveAudio: r.audio
    },
    i.peerConnection = new e(i.pc_config,i.con),
    r.remoteDescriptionSet = !1;
    var o = function(e) {
        if (r.maxVideoBW) {
            var t = e.match(/m=video.*\r\n/);
            if (null == t && (t = e.match(/m=video.*\n/)),
            t && 0 < t.length) {
                var n = t[0] + "b=AS:" + r.maxVideoBW + "\r\n";
                e = e.replace(t[0], n)
            }
        }
        return r.maxAudioBW && (null == (t = e.match(/m=audio.*\r\n/)) && (t = e.match(/m=audio.*\n/)),
        t && 0 < t.length && (n = t[0] + "b=AS:" + r.maxAudioBW + "\r\n",
        e = e.replace(t[0], n))),
        e
    };
    i.close = function() {
        i.state = "closed",
        i.peerConnection.close()
    }
    ,
    r.localCandidates = [],
    i.peerConnection.onicecandidate = function(e) {
        e.candidate ? (e.candidate.candidate.match(/a=/) || (e.candidate.candidate = "a=" + e.candidate.candidate),
        r.remoteDescriptionSet ? r.callback({
            type: "candidate",
            candidate: e.candidate
        }) : r.localCandidates.push(e.candidate)) : console.log("End of candidates.", i.peerConnection.localDescription)
    }
    ,
    i.peerConnection.onaddstream = function(e) {
        i.onaddstream && i.onaddstream(e)
    }
    ,
    i.peerConnection.onremovestream = function(e) {
        i.onremovestream && i.onremovestream(e)
    }
    ;
    var t, a = function(e) {
        console.log("Error in Stack ", e)
    }, n = function(e) {
        e.sdp = o(e.sdp),
        console.log("Set local description", e.sdp),
        t = e,
        i.peerConnection.setLocalDescription(t, function() {
            console.log("The final LocalDesc", i.peerConnection.localDescription),
            r.callback(i.peerConnection.localDescription)
        }, a)
    }, s = function(e) {
        e.sdp = o(e.sdp),
        r.callback(e),
        t = e,
        i.peerConnection.setLocalDescription(e)
    };
    return i.createOffer = function(e) {
        !0 === e ? i.peerConnection.createOffer(n, a, i.mediaConstraints) : i.peerConnection.createOffer(n, a)
    }
    ,
    i.addStream = function(e) {
        i.peerConnection.addStream(e)
    }
    ,
    r.remoteCandidates = [],
    i.processSignalingMessage = function(t) {
        if (console.log("Process Signaling Message", t),
        "offer" === t.type)
            t.sdp = o(t.sdp),
            i.peerConnection.setRemoteDescription(new RTCSessionDescription(t)),
            i.peerConnection.createAnswer(s, null, i.mediaConstraints),
            r.remoteDescriptionSet = !0;
        else if ("answer" === t.type)
            console.log("Set remote description", t.sdp),
            t.sdp = o(t.sdp),
            i.peerConnection.setRemoteDescription(new RTCSessionDescription(t), function() {
                for (r.remoteDescriptionSet = !0,
                console.log("Candidates to be added: ", r.remoteCandidates.length); 0 < r.remoteCandidates.length; )
                    console.log("Candidate :", r.remoteCandidates[r.remoteCandidates.length - 1]),
                    i.peerConnection.addIceCandidate(r.remoteCandidates.shift(), function() {}, a);
                for (; 0 < r.localCandidates.length; )
                    r.callback({
                        type: "candidate",
                        candidate: r.localCandidates.shift()
                    })
            }, function() {
                console.log("Error Setting Remote Description")
            });
        else if ("candidate" === t.type) {
            console.log("Message with candidate");
            try {
                var e;
                (e = "object" == typeof t.candidate ? t.candidate : JSON.parse(t.candidate)).candidate = e.candidate.replace(/a=/g, ""),
                e.sdpMLineIndex = parseInt(e.sdpMLineIndex),
                e.sdpMLineIndex = "audio" == e.sdpMid ? 0 : 1;
                var n = new RTCIceCandidate(e);
                console.log("Remote Candidate", n),
                r.remoteDescriptionSet ? i.peerConnection.addIceCandidate(n, function() {}, a) : r.remoteCandidates.push(n)
            } catch (e) {
                L.Logger.error("Error parsing candidate", t.candidate)
            }
        }
    }
    ,
    i
}
,
FmVideoPrivate = FmVideoPrivate || {},
FmVideoPrivate.FirefoxStack = function(r) {
    var i = {}
      , o = mozRTCSessionDescription
      , a = mozRTCIceCandidate;
    i.pc_config = {
        iceServers: []
    },
    void 0 !== r.iceServers && (i.pc_config.iceServers = r.iceServers),
    void 0 === r.audio && (r.audio = !0),
    void 0 === r.video && (r.video = !0),
    i.mediaConstraints = {
        offerToReceiveAudio: r.audio,
        offerToReceiveVideo: r.video,
        mozDontOfferDataChannel: !0
    };
    var t = function(e) {
        L.Logger.error("Error in Stack ", e)
    }
      , s = !1;
    i.peerConnection = new mozRTCPeerConnection(i.pc_config,i.con),
    r.localCandidates = [],
    i.peerConnection.onicecandidate = function(e) {
        var t = {};
        e.candidate ? (s = !0,
        e.candidate.candidate.match(/a=/) || (e.candidate.candidate = "a=" + e.candidate.candidate),
        t = e.candidate,
        r.remoteDescriptionSet ? r.callback({
            type: "candidate",
            candidate: t
        }) : (r.localCandidates.push(t),
        L.Logger.debug("Local Candidates stored: ", r.localCandidates.length, r.localCandidates))) : L.Logger.info("Gathered all candidates. Sending END candidate")
    }
    ,
    i.peerConnection.onaddstream = function(e) {
        i.onaddstream && i.onaddstream(e)
    }
    ,
    i.peerConnection.onremovestream = function(e) {
        i.onremovestream && i.onremovestream(e)
    }
    ,
    i.peerConnection.oniceconnectionstatechange = function(e) {
        i.oniceconnectionstatechange && i.oniceconnectionstatechange(e.target.iceConnectionState)
    }
    ;
    var c, d = function(e) {
        if (r.video && r.maxVideoBW) {
            var t = (e = e.replace(/b=AS:.*\r\n/g, "")).match(/m=video.*\r\n/);
            if (null == t && (t = e.match(/m=video.*\n/)),
            t && 0 < t.length) {
                var n = t[0] + "b=AS:" + r.maxVideoBW + "\r\n";
                e = e.replace(t[0], n)
            }
        }
        return r.audio && r.maxAudioBW && (null == (t = e.match(/m=audio.*\r\n/)) && (t = e.match(/m=audio.*\n/)),
        t && 0 < t.length && (n = t[0] + "b=AS:" + r.maxAudioBW + "\r\n",
        e = e.replace(t[0], n))),
        e
    }, n = function(e) {
        e.sdp = d(e.sdp),
        e.sdp = e.sdp.replace(/a=ice-options:google-ice\r\n/g, ""),
        r.callback(e),
        c = e
    }, u = function(e) {
        e.sdp = d(e.sdp),
        e.sdp = e.sdp.replace(/a=ice-options:google-ice\r\n/g, ""),
        r.callback(e),
        c = e,
        i.peerConnection.setLocalDescription(c)
    };
    return i.updateSpec = function(e) {
        (e.maxVideoBW || e.maxAudioBW) && (e.maxVideoBW && (L.Logger.debug("Maxvideo Requested", e.maxVideoBW, "limit", r.limitMaxVideoBW),
        e.maxVideoBW > r.limitMaxVideoBW && (e.maxVideoBW = r.limitMaxVideoBW),
        r.maxVideoBW = e.maxVideoBW,
        L.Logger.debug("Result", r.maxVideoBW)),
        e.maxAudioBW && (e.maxAudioBW > r.limitMaxAudioBW && (e.maxAudioBW = r.limitMaxAudioBW),
        r.maxAudioBW = e.maxAudioBW),
        c.sdp = d(c.sdp),
        e.Sdp ? L.Logger.error("Cannot update with renegotiation in Firefox, try without renegotiation") : (L.Logger.debug("Updating without renegotiation, newVideoBW:", r.maxVideoBW, "newAudioBW:", r.maxAudioBW),
        r.callback({
            type: "updatestream",
            sdp: c.sdp
        }))),
        (e.minVideoBW || void 0 !== e.slideShowMode) && (L.Logger.debug("MinVideo Changed to ", e.minVideoBW),
        L.Logger.debug("SlideShowMode Changed to ", e.slideShowMode),
        r.callback({
            type: "updatestream",
            config: e
        }))
    }
    ,
    i.createOffer = function(e) {
        !0 === e ? i.peerConnection.createOffer(n, t, i.mediaConstraints) : i.peerConnection.createOffer(n, t)
    }
    ,
    i.addStream = function(e) {
        i.peerConnection.addStream(e)
    }
    ,
    r.remoteCandidates = [],
    r.remoteDescriptionSet = !1,
    i.close = function() {
        i.state = "closed",
        i.peerConnection.close()
    }
    ,
    i.processSignalingMessage = function(t) {
        if ("offer" === t.type)
            t.sdp = d(t.sdp),
            i.peerConnection.setRemoteDescription(new o(t), function() {
                i.peerConnection.createAnswer(u, function(e) {
                    L.Logger.error("Error", e)
                }, i.mediaConstraints),
                r.remoteDescriptionSet = !0
            }, function(e) {
                L.Logger.error("Error setting Remote Description", e)
            });
        else if ("answer" === t.type)
            L.Logger.info("Set remote and local description"),
            L.Logger.debug("Local Description to set", c.sdp),
            L.Logger.debug("Remote Description to set", t.sdp),
            t.sdp = d(t.sdp),
            i.peerConnection.setLocalDescription(c, function() {
                i.peerConnection.setRemoteDescription(new o(t), function() {
                    for (r.remoteDescriptionSet = !0,
                    L.Logger.info("Remote Description successfully set"); 0 < r.remoteCandidates.length && s; )
                        L.Logger.info("Setting stored remote candidates"),
                        i.peerConnection.addIceCandidate(r.remoteCandidates.shift());
                    for (; 0 < r.localCandidates.length; )
                        L.Logger.info("Sending Candidate from list"),
                        r.callback({
                            type: "candidate",
                            candidate: r.localCandidates.shift()
                        })
                }, function(e) {
                    L.Logger.error("Error Setting Remote Description", e)
                })
            }, function(e) {
                L.Logger.error("Failure setting Local Description", e)
            });
        else if ("candidate" === t.type)
            try {
                var e;
                (e = "object" == typeof t.candidate ? t.candidate : JSON.parse(t.candidate)).candidate = e.candidate.replace(/ generation 0/g, ""),
                e.candidate = e.candidate.replace(/ udp /g, " UDP "),
                e.sdpMLineIndex = parseInt(e.sdpMLineIndex);
                var n = new a(e);
                if (r.remoteDescriptionSet && s)
                    for (i.peerConnection.addIceCandidate(n); 0 < r.remoteCandidates.length; )
                        L.Logger.info("Setting stored remote candidates"),
                        i.peerConnection.addIceCandidate(r.remoteCandidates.shift());
                else
                    r.remoteCandidates.push(n)
            } catch (e) {
                L.Logger.error("Error parsing candidate", t.candidate, e)
            }
    }
    ,
    i
}
,
FmVideoPrivate = FmVideoPrivate || {},
FmVideoPrivate.ChromeStableStack = function(r) {
    var i = {
        pc_config: {
            iceServers: []
        },
        con: {
            optional: [{
                DtlsSrtpKeyAgreement: !0
            }]
        }
    };
    void 0 !== r.iceServers && (i.pc_config.iceServers = r.iceServers),
    void 0 === r.audio && (r.audio = !0),
    void 0 === r.video && (r.video = !0),
    i.mediaConstraints = {
        mandatory: {
            OfferToReceiveVideo: r.video,
            OfferToReceiveAudio: r.audio
        }
    };
    var t = function(e) {
        L.Logger.error("Error in Stack ", e)
    };
    i.peerConnection = new webkitRTCPeerConnection(i.pc_config,i.con);
    var o = function(e) {
        if (r.video && r.maxVideoBW) {
            var t = (e = e.replace(/b=AS:.*\r\n/g, "")).match(/m=video.*\r\n/);
            if (null == t && (t = e.match(/m=video.*\n/)),
            t && 0 < t.length) {
                var n = t[0] + "b=AS:" + r.maxVideoBW + "\r\n";
                e = e.replace(t[0], n)
            }
        }
        return r.audio && r.maxAudioBW && (null == (t = e.match(/m=audio.*\r\n/)) && (t = e.match(/m=audio.*\n/)),
        t && 0 < t.length && (n = t[0] + "b=AS:" + r.maxAudioBW + "\r\n",
        e = e.replace(t[0], n))),
        e
    };
    i.close = function() {
        i.state = "closed",
        i.peerConnection.close()
    }
    ,
    r.localCandidates = [],
    i.peerConnection.onicecandidate = function(e) {
        var t = {};
        e.candidate ? (e.candidate.candidate.match(/a=/) || (e.candidate.candidate = "a=" + e.candidate.candidate),
        t = {
            sdpMLineIndex: e.candidate.sdpMLineIndex,
            sdpMid: e.candidate.sdpMid,
            candidate: e.candidate.candidate
        }) : (L.Logger.info("Gathered all candidates. Sending END candidate"),
        t = {
            sdpMLineIndex: -1,
            sdpMid: "end",
            candidate: "end"
        }),
        r.remoteDescriptionSet ? r.callback({
            type: "candidate",
            candidate: t
        }) : (r.localCandidates.push(t),
        L.Logger.info("Storing candidate: ", r.localCandidates.length, t))
    }
    ,
    i.peerConnection.onaddstream = function(e) {
        i.onaddstream && i.onaddstream(e)
    }
    ,
    i.peerConnection.onremovestream = function(e) {
        i.onremovestream && i.onremovestream(e)
    }
    ,
    i.peerConnection.oniceconnectionstatechange = function(e) {
        i.oniceconnectionstatechange && i.oniceconnectionstatechange(e.target.iceConnectionState)
    }
    ;
    var a, s, n = function(e) {
        e.sdp = o(e.sdp),
        e.sdp = e.sdp.replace(/a=ice-options:google-ice\r\n/g, ""),
        r.callback({
            type: e.type,
            sdp: e.sdp
        }),
        a = e
    }, c = function(e) {
        e.sdp = o(e.sdp),
        r.callback({
            type: e.type,
            sdp: e.sdp
        }),
        a = e,
        i.peerConnection.setLocalDescription(e)
    };
    return i.updateSpec = function(e, t) {
        (e.maxVideoBW || e.maxAudioBW) && (e.maxVideoBW && (L.Logger.debug("Maxvideo Requested", e.maxVideoBW, "limit", r.limitMaxVideoBW),
        e.maxVideoBW > r.limitMaxVideoBW && (e.maxVideoBW = r.limitMaxVideoBW),
        r.maxVideoBW = e.maxVideoBW,
        L.Logger.debug("Result", r.maxVideoBW)),
        e.maxAudioBW && (e.maxAudioBW > r.limitMaxAudioBW && (e.maxAudioBW = r.limitMaxAudioBW),
        r.maxAudioBW = e.maxAudioBW),
        a.sdp = o(a.sdp),
        e.Sdp || e.maxAudioBW ? (L.Logger.debug("Updating with SDP renegotiation", r.maxVideoBW),
        i.peerConnection.setLocalDescription(a, function() {
            s.sdp = o(s.sdp),
            i.peerConnection.setRemoteDescription(new RTCSessionDescription(s), function() {
                r.remoteDescriptionSet = !0,
                r.callback({
                    type: "updatestream",
                    sdp: a.sdp
                })
            })
        }, function(e) {
            L.Logger.error("Error updating configuration", e),
            t("error")
        })) : (L.Logger.debug("Updating without SDP renegotiation, newVideoBW:", r.maxVideoBW, "newAudioBW:", r.maxAudioBW),
        r.callback({
            type: "updatestream",
            sdp: a.sdp
        }))),
        (e.minVideoBW || void 0 !== e.slideShowMode) && (L.Logger.debug("MinVideo Changed to ", e.minVideoBW),
        L.Logger.debug("SlideShowMode Changed to ", e.slideShowMode),
        r.callback({
            type: "updatestream",
            config: e
        }))
    }
    ,
    i.createOffer = function(e) {
        !0 === e ? i.peerConnection.createOffer(n, t, i.mediaConstraints) : i.peerConnection.createOffer(n, t)
    }
    ,
    i.addStream = function(e) {
        i.peerConnection.addStream(e)
    }
    ,
    r.remoteCandidates = [],
    r.remoteDescriptionSet = !1,
    i.processSignalingMessage = function(t) {
        if ("offer" === t.type)
            t.sdp = o(t.sdp),
            i.peerConnection.setRemoteDescription(new RTCSessionDescription(t), function() {
                i.peerConnection.createAnswer(c, function(e) {
                    L.Logger.error("Error: ", e)
                }, i.mediaConstraints),
                r.remoteDescriptionSet = !0
            }, function(e) {
                L.Logger.error("Error setting Remote Description", e)
            });
        else if ("answer" === t.type)
            L.Logger.info("Set remote and local description"),
            L.Logger.debug("Remote Description", t.sdp),
            L.Logger.debug("Local Description", a.sdp),
            t.sdp = o(t.sdp),
            s = t,
            i.peerConnection.setLocalDescription(a, function() {
                i.peerConnection.setRemoteDescription(new RTCSessionDescription(t), function() {
                    for (r.remoteDescriptionSet = !0,
                    L.Logger.info("Candidates to be added: ", r.remoteCandidates.length, r.remoteCandidates); 0 < r.remoteCandidates.length; )
                        i.peerConnection.addIceCandidate(r.remoteCandidates.shift());
                    for (L.Logger.info("Local candidates to send:", r.localCandidates.length); 0 < r.localCandidates.length; )
                        r.callback({
                            type: "candidate",
                            candidate: r.localCandidates.shift()
                        })
                })
            });
        else if ("candidate" === t.type)
            try {
                var e;
                (e = "object" == typeof t.candidate ? t.candidate : JSON.parse(t.candidate)).candidate = e.candidate.replace(/a=/g, ""),
                e.sdpMLineIndex = parseInt(e.sdpMLineIndex);
                var n = new RTCIceCandidate(e);
                r.remoteDescriptionSet ? i.peerConnection.addIceCandidate(n) : r.remoteCandidates.push(n)
            } catch (e) {
                L.Logger.error("Error parsing candidate", t.candidate)
            }
    }
    ,
    i
}
,
FmVideoPrivate = FmVideoPrivate || {},
FmVideoPrivate.ChromeCanaryStack = function(r) {
    var i = {}
      , e = webkitRTCPeerConnection;
    i.pc_config = {
        iceServers: []
    },
    i.con = {
        optional: [{
            DtlsSrtpKeyAgreement: !0
        }]
    },
    void 0 !== r.stunServerUrl && i.pc_config.iceServers.push({
        url: r.stunServerUrl
    }),
    (r.turnServer || {}).url && i.pc_config.iceServers.push({
        username: r.turnServer.username,
        credential: r.turnServer.password,
        url: r.turnServer.url
    }),
    (void 0 === r.audio || r.nop2p) && (r.audio = !0),
    (void 0 === r.video || r.nop2p) && (r.video = !0),
    i.mediaConstraints = {
        mandatory: {
            OfferToReceiveVideo: r.video,
            OfferToReceiveAudio: r.audio
        }
    },
    i.roapSessionId = 103,
    i.peerConnection = new e(i.pc_config,i.con),
    i.peerConnection.onicecandidate = function(e) {
        L.Logger.debug("PeerConnection: ", r.session_id),
        e.candidate ? i.iceCandidateCount += 1 : (L.Logger.debug("State: " + i.peerConnection.iceGatheringState),
        void 0 === i.ices && (i.ices = 0),
        i.ices += 1,
        1 <= i.ices && i.moreIceComing && (i.moreIceComing = !1,
        i.markActionNeeded()))
    }
    ;
    var t = function(e) {
        if (r.maxVideoBW) {
            var t = e.match(/m=video.*\r\n/);
            if (t && 0 < t.length) {
                var n = t[0] + "b=AS:" + r.maxVideoBW + "\r\n";
                e = e.replace(t[0], n)
            }
        }
        return r.maxAudioBW && (t = e.match(/m=audio.*\r\n/)) && 0 < t.length && (n = t[0] + "b=AS:" + r.maxAudioBW + "\r\n",
        e = e.replace(t[0], n)),
        e
    };
    return i.processSignalingMessage = function(e) {
        L.Logger.debug("Activity on conn " + i.sessionId),
        e = JSON.parse(e),
        i.incomingMessage = e,
        "new" === i.state ? "OFFER" === e.messageType ? (e = {
            sdp: e.sdp,
            type: "offer"
        },
        i.peerConnection.setRemoteDescription(new RTCSessionDescription(e)),
        i.state = "offer-received",
        i.markActionNeeded()) : i.error("Illegal message for this state: " + e.messageType + " in state " + i.state) : "offer-sent" === i.state ? "ANSWER" === e.messageType ? (e = {
            sdp: e.sdp,
            type: "answer"
        },
        L.Logger.debug("Received ANSWER: ", e.sdp),
        e.sdp = t(e.sdp),
        i.peerConnection.setRemoteDescription(new RTCSessionDescription(e)),
        i.sendOK(),
        i.state = "established") : "pr-answer" === e.messageType ? (e = {
            sdp: e.sdp,
            type: "pr-answer"
        },
        i.peerConnection.setRemoteDescription(new RTCSessionDescription(e))) : "offer" === e.messageType ? i.error("Not written yet") : i.error("Illegal message for this state: " + e.messageType + " in state " + i.state) : "established" === i.state && ("OFFER" === e.messageType ? (e = {
            sdp: e.sdp,
            type: "offer"
        },
        i.peerConnection.setRemoteDescription(new RTCSessionDescription(e)),
        i.state = "offer-received",
        i.markActionNeeded()) : i.error("Illegal message for this state: " + e.messageType + " in state " + i.state))
    }
    ,
    i.addStream = function(e) {
        i.peerConnection.addStream(e),
        i.markActionNeeded()
    }
    ,
    i.removeStream = function() {
        i.markActionNeeded()
    }
    ,
    i.close = function() {
        i.state = "closed",
        i.peerConnection.close()
    }
    ,
    i.markActionNeeded = function() {
        i.actionNeeded = !0,
        i.doLater(function() {
            i.onstablestate()
        })
    }
    ,
    i.doLater = function(e) {
        window.setTimeout(e, 1)
    }
    ,
    i.onstablestate = function() {
        var e;
        if (i.actionNeeded) {
            if ("new" === i.state || "established" === i.state)
                i.peerConnection.createOffer(function(e) {
                    e.sdp = t(e.sdp),
                    L.Logger.debug("Changed", e.sdp),
                    e.sdp !== i.prevOffer ? (i.peerConnection.setLocalDescription(e),
                    i.state = "preparing-offer",
                    i.markActionNeeded()) : L.Logger.debug("Not sending a new offer")
                }, null, i.mediaConstraints);
            else if ("preparing-offer" === i.state) {
                if (i.moreIceComing)
                    return;
                i.prevOffer = i.peerConnection.localDescription.sdp,
                L.Logger.debug("Sending OFFER: " + i.prevOffer),
                i.sendMessage("OFFER", i.prevOffer),
                i.state = "offer-sent"
            } else if ("offer-received" === i.state)
                i.peerConnection.createAnswer(function(e) {
                    i.peerConnection.setLocalDescription(e),
                    i.state = "offer-received-preparing-answer",
                    i.iceStarted ? i.markActionNeeded() : (L.Logger.debug((new Date).getTime() + ": Starting ICE in responder"),
                    i.iceStarted = !0)
                }, null, i.mediaConstraints);
            else if ("offer-received-preparing-answer" === i.state) {
                if (i.moreIceComing)
                    return;
                e = i.peerConnection.localDescription.sdp,
                i.sendMessage("ANSWER", e),
                i.state = "established"
            } else
                i.error("Dazed and confused in state " + i.state + ", stopping here");
            i.actionNeeded = !1
        }
    }
    ,
    i.sendOK = function() {
        i.sendMessage("OK")
    }
    ,
    i.sendMessage = function(e, t) {
        var n = {};
        n.messageType = e,
        n.sdp = t,
        "OFFER" === e ? (n.offererSessionId = i.sessionId,
        n.answererSessionId = i.otherSessionId,
        n.seq = i.sequenceNumber += 1,
        n.tiebreaker = Math.floor(429496723 * Math.random() + 1)) : (n.offererSessionId = i.incomingMessage.offererSessionId,
        n.answererSessionId = i.sessionId,
        n.seq = i.incomingMessage.seq),
        i.onsignalingmessage(JSON.stringify(n))
    }
    ,
    i.error = function(e) {
        throw "Error in RoapOnJsep: " + e
    }
    ,
    i.sessionId = i.roapSessionId += 1,
    i.sequenceNumber = 0,
    i.actionNeeded = !1,
    i.iceStarted = !1,
    i.moreIceComing = !0,
    i.iceCandidateCount = 0,
    i.onsignalingmessage = r.callback,
    i.peerConnection.onopen = function() {
        i.onopen && i.onopen()
    }
    ,
    i.peerConnection.onaddstream = function(e) {
        i.onaddstream && i.onaddstream(e)
    }
    ,
    i.peerConnection.onremovestream = function(e) {
        i.onremovestream && i.onremovestream(e)
    }
    ,
    i.peerConnection.oniceconnectionstatechange = function(e) {
        i.oniceconnectionstatechange && i.oniceconnectionstatechange(e.currentTarget.iceConnectionState)
    }
    ,
    i.onaddstream = null,
    i.onremovestream = null,
    i.state = "new",
    i.markActionNeeded(),
    i
}
,
FmVideoPrivate = FmVideoPrivate || {},
FmVideoPrivate.sessionId = 103,
FmVideoPrivate.Connection = function(e) {
    var t = {};
    if (e.session_id = FmVideoPrivate.sessionId += 1,
    t.browser = FmVideoPrivate.getBrowser(),
    "fake" === t.browser)
        L.Logger.warn("Publish/subscribe video/audio streams not supported in erizofc yet"),
        t = FmVideoPrivate.FcStack(e);
    else if ("mozilla" === t.browser)
        L.Logger.debug("Firefox Stack"),
        t = FmVideoPrivate.FirefoxStack(e);
    else if ("bowser" === t.browser)
        L.Logger.debug("Bowser Stack"),
        t = FmVideoPrivate.BowserStack(e);
    else {
        if ("chrome-stable" !== t.browser)
            throw L.Logger.error("No stack available for this browser"),
            "WebRTC stack not available";
        L.Logger.debug("Chrome Stable Stack"),
        t = FmVideoPrivate.ChromeStableStack(e)
    }
    return t.updateSpec || (t.updateSpec = function(e, t) {
        L.Logger.error("Update Configuration not implemented in this browser"),
        t && t("unimplemented")
    }
    ),
    t
}
,
FmVideoPrivate.getBrowser = function() {
    var e = "none";
    return "undefined" != typeof module && module.exports ? e = "fake" : null !== window.navigator.userAgent.match("Firefox") ? e = "mozilla" : null !== window.navigator.userAgent.match("Bowser") ? e = "bowser" : null !== window.navigator.userAgent.match("Chrome") ? 26 <= window.navigator.appVersion.match(/Chrome\/([\w\W]*?)\./)[1] && (e = "chrome-stable") : null !== window.navigator.userAgent.match("Safari") ? e = "bowser" : null !== window.navigator.userAgent.match("AppleWebKit") && (e = "bowser"),
    e
}
,
FmVideoPrivate.GetUserMedia = function(n, r, i) {
    if (navigator.getMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia,
    n.screen)
        switch (L.Logger.debug("Screen access requested"),
        FmVideoPrivate.getBrowser()) {
        case "mozilla":
            L.Logger.debug("Screen sharing in Firefox");
            var e = {};
            null != n.video.mandatory ? (e.video = n.video,
            e.video.mediaSource = "window") : e = {
                audio: n.audio,
                video: {
                    mediaSource: "window"
                }
            },
            navigator.getMedia(e, r, i);
            break;
        case "chrome-stable":
            L.Logger.debug("Screen sharing in Chrome"),
            e = "okeephmleflklcdebijnponpabbmmgeo",
            n.extensionId && (L.Logger.debug("extensionId supplied, using " + n.extensionId),
            e = n.extensionId),
            L.Logger.debug("Screen access on chrome stable, looking for extension");
            try {
                chrome.runtime.sendMessage(e, {
                    getStream: !0
                }, function(e) {
                    var t = {};
                    null == e ? (L.Logger.error("Access to screen denied"),
                    i({
                        code: "Access to screen denied"
                    })) : (e = e.streamId,
                    null != n.video.mandatory ? (t.video = n.video,
                    t.video.mandatory.chromeMediaSource = "desktop",
                    t.video.mandatory.chromeMediaSourceId = e) : t = {
                        video: {
                            mandatory: {
                                chromeMediaSource: "desktop",
                                chromeMediaSourceId: e
                            }
                        }
                    },
                    navigator.getMedia(t, r, i))
                })
            } catch (e) {
                L.Logger.debug("Screensharing plugin is not accessible "),
                i({
                    code: "no_plugin_present"
                });
                break
            }
            break;
        default:
            L.Logger.error("This browser does not support ScreenSharing")
        }
    else
        "undefined" != typeof module && module.exports ? L.Logger.error("Video/audio streams not supported in erizofc yet") : navigator.getMedia(n, r, i)
}
,
FmVideoPrivate = FmVideoPrivate || {},
FmVideoPrivate.Stream = function(r) {
    var n, c = FmVideoPrivate.EventDispatcher(r);
    if (c.stream = r.stream,
    c.url = r.url,
    c.recording = r.recording,
    c.room = void 0,
    c.showing = !1,
    c.local = !1,
    c.video = r.video,
    c.audio = r.audio,
    c.screen = r.screen,
    c.videoSize = r.videoSize,
    c.extensionId = r.extensionId,
    !(void 0 === c.videoSize || c.videoSize instanceof Array && 4 == c.videoSize.length))
        throw Error("Invalid Video Size");
    return void 0 !== r.local && !0 !== r.local || (c.local = !0),
    c.getID = function() {
        return c.local && !r.streamID ? "local" : r.streamID
    }
    ,
    c.getAttributes = function() {
        return r.attributes
    }
    ,
    c.setAttributes = function() {
        L.Logger.error("Failed to set attributes data. This Stream object has not been published.")
    }
    ,
    c.updateLocalAttributes = function(e) {
        r.attributes = e
    }
    ,
    c.hasAudio = function() {
        return r.audio
    }
    ,
    c.hasVideo = function() {
        return r.video
    }
    ,
    c.hasData = function() {
        return r.data
    }
    ,
    c.hasScreen = function() {
        return r.screen
    }
    ,
    c.sendData = function() {
        L.Logger.error("Failed to send data. This Stream object has not that channel enabled.")
    }
    ,
    c.init = function() {
        try {
            if ((r.audio || r.video || r.screen) && void 0 === r.url) {
                L.Logger.info("Requested access to local media");
                var e = r.video;
                1 != e && 1 != r.screen || void 0 === c.videoSize ? 1 == r.screen && void 0 === e && (e = !0) : e = {
                    mandatory: {
                        minWidth: c.videoSize[0],
                        minHeight: c.videoSize[1],
                        maxWidth: c.videoSize[2],
                        maxHeight: c.videoSize[3]
                    }
                };
                var t = {
                    video: e,
                    audio: r.audio,
                    fake: r.fake,
                    screen: r.screen,
                    extensionId: c.extensionId
                };
                L.Logger.debug(t),
                FmVideoPrivate.GetUserMedia(t, function(e) {
                    L.Logger.info("User has granted access to local media."),
                    c.stream = e,
                    e = FmVideoPrivate.StreamEvent({
                        type: "access-accepted"
                    }),
                    c.dispatchEvent(e)
                }, function(e) {
                    L.Logger.error("Failed to get access to local media. Error code was " + e.code + "."),
                    e = FmVideoPrivate.StreamEvent({
                        type: "access-denied",
                        msg: e
                    }),
                    c.dispatchEvent(e)
                })
            } else {
                var n = FmVideoPrivate.StreamEvent({
                    type: "access-accepted"
                });
                c.dispatchEvent(n)
            }
        } catch (e) {
            L.Logger.error("Failed to get access to local media. Error was " + e + "."),
            n = FmVideoPrivate.StreamEvent({
                type: "access-denied",
                msg: e
            }),
            c.dispatchEvent(n)
        }
    }
    ,
    c.close = function() {
        c.local && (void 0 !== c.room && c.room.unpublish(c),
        c.hide(),
        void 0 !== c.stream && c.stream.getTracks().forEach(function(e) {
            e.stop()
        }),
        c.stream = void 0)
    }
    ,
    c.play = function(e, t) {
        if (t = t || {},
        c.elementID = e,
        c.hasVideo() || this.hasScreen()) {
            if (void 0 !== e) {
                var n = new FmVideoPrivate.VideoPlayer({
                    id: c.getID(),
                    stream: c,
                    elementID: e,
                    options: t
                });
                c.player = n,
                c.showing = !0
            }
        } else
            c.hasAudio && (n = new FmVideoPrivate.AudioPlayer({
                id: c.getID(),
                stream: c,
                elementID: e,
                options: t
            }),
            c.player = n,
            c.showing = !0)
    }
    ,
    c.stop = function() {
        c.showing && void 0 !== c.player && (c.player.destroy(),
        c.showing = !1)
    }
    ,
    c.show = c.play,
    c.hide = c.stop,
    n = function() {
        if (void 0 !== c.player && void 0 !== c.stream) {
            var e = c.player.video
              , t = document.defaultView.getComputedStyle(e)
              , n = parseInt(t.getPropertyValue("width"), 10)
              , r = parseInt(t.getPropertyValue("height"), 10)
              , i = parseInt(t.getPropertyValue("left"), 10)
              , o = (t = parseInt(t.getPropertyValue("top"), 10),
            document.getElementById(c.elementID))
              , a = document.defaultView.getComputedStyle(o)
              , s = (o = parseInt(a.getPropertyValue("width"), 10),
            a = parseInt(a.getPropertyValue("height"), 10),
            document.createElement("canvas"));
            return s.id = "testing",
            s.width = o,
            s.height = a,
            s.setAttribute("style", "display: none"),
            s.getContext("2d").drawImage(e, i, t, n, r),
            s
        }
        return null
    }
    ,
    c.getVideoFrameURL = function(e) {
        var t = n();
        return null !== t ? e ? t.toDataURL(e) : t.toDataURL() : null
    }
    ,
    c.getVideoFrame = function() {
        var e = n();
        return null !== e ? e.getContext("2d").getImageData(0, 0, e.width, e.height) : null
    }
    ,
    c.checkOptions = function(e, t) {
        !0 === t ? (e.video || e.audio || e.screen) && (L.Logger.warning("Cannot update type of subscription"),
        e.video = void 0,
        e.audio = void 0,
        e.screen = void 0) : !1 === c.local && (!0 === e.video && !1 === c.hasVideo() && (L.Logger.warning("Trying to subscribe to video when there is no video, won't subscribe to video"),
        e.video = !1),
        !0 === e.audio && !1 === c.hasAudio()) && (L.Logger.warning("Trying to subscribe to audio when there is no audio, won't subscribe to audio"),
        e.audio = !1),
        !1 === c.local && !c.hasVideo() && !0 === e.slideShowMode && (L.Logger.warning("Cannot enable slideShowMode if it is not a video subscription, please check your parameters"),
        e.slideShowMode = !1)
    }
    ,
    c.updateConfiguration = function(e, t) {
        if (void 0 !== e) {
            if (!c.pc)
                return "This stream has no peerConnection attached, ignoring";
            if (c.checkOptions(e, !0),
            c.local)
                if (c.room.p2p)
                    for (var n in c.pc)
                        c.pc[n].updateSpec(e, t);
                else
                    c.pc.updateSpec(e, t);
            else
                c.pc.updateSpec(e, t)
        }
    }
    ,
    c
}
,
FmVideoPrivate = FmVideoPrivate || {},
FmVideoPrivate.Room = function(a) {
    var t, i, o, s, c, d, u = FmVideoPrivate.EventDispatcher(a);
    return u.remoteStreams = {},
    u.localStreams = {},
    u.roomID = "",
    u.socket = {},
    u.state = 0,
    u.p2p = !1,
    u.addEventListener("room-disconnected", function() {
        var e, t;
        for (e in u.state = 0,
        u.remoteStreams)
            u.remoteStreams.hasOwnProperty(e) && (t = u.remoteStreams[e],
            d(t),
            delete u.remoteStreams[e],
            t = FmVideoPrivate.StreamEvent({
                type: "stream-removed",
                stream: t
            }),
            u.dispatchEvent(t));
        for (e in u.remoteStreams = {},
        u.localStreams)
            if (u.localStreams.hasOwnProperty(e)) {
                if (t = u.localStreams[e],
                u.p2p)
                    for (var n in t.pc)
                        t.pc[n].close();
                else
                    t.pc.close();
                delete u.localStreams[e]
            }
        try {
            u.socket.disconnect()
        } catch (e) {
            L.Logger.debug("Socket already disconnected")
        }
        u.socket = void 0
    }),
    d = function(e) {
        void 0 !== e.stream && (e.hide(),
        e.pc && e.pc.close(),
        e.local && e.stream.stop())
    }
    ,
    s = function(e, t) {
        e.local ? i("sendDataStream", {
            id: e.getID(),
            msg: t
        }) : L.Logger.error("You can not send data through a remote stream")
    }
    ,
    c = function(e, t) {
        e.local ? (e.updateLocalAttributes(t),
        i("updateStreamAttributes", {
            id: e.getID(),
            attrs: t
        })) : L.Logger.error("You can not update attributes in a remote stream")
    }
    ,
    t = function(e, t, n) {
        u.socket = io0.connect(e.host, {
            reconnect: !1,
            secure: e.secure,
            "force new connection": !0,
            transports: ["websocket"]
        }),
        u.socket.on("onAddStream", function(e) {
            var t = FmVideoPrivate.Stream({
                streamID: e.id,
                local: !1,
                audio: e.audio,
                video: e.video,
                data: e.data,
                screen: e.screen,
                attributes: e.attributes
            });
            u.remoteStreams[e.id] = t,
            e = FmVideoPrivate.StreamEvent({
                type: "stream-added",
                stream: t
            }),
            u.dispatchEvent(e)
        }),
        u.socket.on("signaling_message_erizo", function(e) {
            var t;
            (t = e.peerId ? u.remoteStreams[e.peerId] : u.localStreams[e.streamId]) && t.pc.processSignalingMessage(e.mess)
        }),
        u.socket.on("signaling_message_peer", function(e) {
            var t = u.localStreams[e.streamId];
            t ? t.pc[e.peerSocket].processSignalingMessage(e.msg) : ((t = u.remoteStreams[e.streamId]).pc || r(t, e.peerSocket),
            t.pc.processSignalingMessage(e.msg))
        }),
        u.socket.on("publish_me", function(t) {
            var n = u.localStreams[t.streamId];
            void 0 === n.pc && (n.pc = {}),
            n.pc[t.peerSocket] = FmVideoPrivate.Connection({
                callback: function(e) {
                    o("signaling_message", {
                        streamId: t.streamId,
                        peerSocket: t.peerSocket,
                        msg: e
                    })
                },
                audio: n.hasAudio(),
                video: n.hasVideo(),
                iceServers: u.iceServers
            }),
            n.pc[t.peerSocket].oniceconnectionstatechange = function(e) {
                "disconnected" === e && (n.pc[t.peerSocket].close(),
                delete n.pc[t.peerSocket])
            }
            ,
            n.pc[t.peerSocket].addStream(n.stream),
            n.pc[t.peerSocket].createOffer()
        });
        var r = function(t, n) {
            t.pc = FmVideoPrivate.Connection({
                callback: function(e) {
                    o("signaling_message", {
                        streamId: t.getID(),
                        peerSocket: n,
                        msg: e
                    })
                },
                iceServers: u.iceServers,
                maxAudioBW: a.maxAudioBW,
                maxVideoBW: a.maxVideoBW,
                limitMaxAudioBW: a.maxAudioBW,
                limitMaxVideoBW: a.maxVideoBW
            }),
            t.pc.onaddstream = function(e) {
                L.Logger.info("Stream subscribed"),
                t.stream = e.stream,
                e = FmVideoPrivate.StreamEvent({
                    type: "stream-subscribed",
                    stream: t
                }),
                u.dispatchEvent(e)
            }
        };
        u.socket.on("onBandwidthAlert", function(e) {
            if (L.Logger.info("Bandwidth Alert on", e.streamID, "message", e.message, "BW:", e.bandwidth),
            e.streamID) {
                var t = u.remoteStreams[e.streamID];
                t && (e = FmVideoPrivate.StreamEvent({
                    type: "bandwidth-alert",
                    stream: t,
                    msg: e.message,
                    bandwidth: e.bandwidth
                }),
                t.dispatchEvent(e))
            }
        }),
        u.socket.on("onDataStream", function(e) {
            var t = u.remoteStreams[e.id];
            e = FmVideoPrivate.StreamEvent({
                type: "stream-data",
                msg: e.msg,
                stream: t
            });
            t.dispatchEvent(e)
        }),
        u.socket.on("onUpdateAttributeStream", function(e) {
            var t = u.remoteStreams[e.id]
              , n = FmVideoPrivate.StreamEvent({
                type: "stream-attributes-update",
                attrs: e.attrs,
                stream: t
            });
            t.updateLocalAttributes(e.attrs),
            t.dispatchEvent(n)
        }),
        u.socket.on("onRemoveStream", function(e) {
            var t = u.remoteStreams[e.id];
            void 0 === t ? L.Logger.warning("Received a removeStream for", e.id, "and it has not been registered here, ignoring.") : (delete u.remoteStreams[e.id],
            d(t),
            e = FmVideoPrivate.StreamEvent({
                type: "stream-removed",
                stream: t
            }),
            u.dispatchEvent(e))
        }),
        u.socket.on("disconnect", function() {
            if (L.Logger.info("Socket disconnected, lost connection to FmVideoPrivateController"),
            0 !== u.state) {
                L.Logger.error("Unexpected disconnection from FmVideoPrivateController");
                var e = FmVideoPrivate.RoomEvent({
                    type: "room-disconnected",
                    message: "unexpected-disconnection"
                });
                u.dispatchEvent(e)
            }
        }),
        u.socket.on("connection_failed", function(e) {
            "publish" === e.type ? (L.Logger.error("ICE Connection Failed on publishing stream", e.streamId),
            0 !== u.state && e.streamId && (e = u.localStreams[e.id]) && !e.failed && (e.failed = !0,
            e = FmVideoPrivate.StreamEvent({
                type: "stream-failed",
                msg: "Publishing local stream failed ICE Checks",
                stream: e
            }),
            u.dispatchEvent(e))) : (L.Logger.error("ICE Connection Failed on subscribe, alerting"),
            0 !== u.state && e.streamId && (e = remoteStreams[e.streamId]) && !e.failed && (e.failed = !0,
            e = FmVideoPrivate.StreamEvent({
                type: "stream-failed",
                msg: "Subscriber failed the ICE Checks, cannot reach Licode for media",
                stream: e
            }),
            u.dispatchEvent(e)))
        }),
        u.socket.on("error", function(e) {
            n("Cannot connect to FmVideoPrivateController (socket.io error)", e)
        }),
        i("token", e, t, n)
    }
    ,
    i = function(e, t, n, r) {
        u.socket.emit(e, t, function(e, t) {
            "success" === e ? void 0 !== n && n(t) : "error" === e ? void 0 !== r && r(t) : void 0 !== n && n(e, t)
        })
    }
    ,
    o = function(e, t, n, r) {
        u.socket.emit(e, t, n, function(e, t) {
            void 0 !== r && r(e, t)
        })
    }
    ,
    u.connect = function() {
        var e = L.Base64.decodeBase64(a.token);
        0 !== u.state && L.Logger.warning("Room already connected"),
        u.state = 1,
        t(JSON.parse(e), function(e) {
            var t, n, r, i = 0, o = [];
            for (i in t = e.streams || [],
            u.p2p = e.p2p,
            n = e.id,
            u.iceServers = e.iceServers,
            u.state = 2,
            a.defaultVideoBW = e.defaultVideoBW,
            a.maxVideoBW = e.maxVideoBW,
            t)
                t.hasOwnProperty(i) && (r = t[i],
                e = FmVideoPrivate.Stream({
                    streamID: r.id,
                    local: !1,
                    audio: r.audio,
                    video: r.video,
                    data: r.data,
                    screen: r.screen,
                    attributes: r.attributes
                }),
                o.push(e),
                u.remoteStreams[r.id] = e);
            u.roomID = n,
            L.Logger.info("Connected to room " + u.roomID),
            i = FmVideoPrivate.RoomEvent({
                type: "room-connected",
                streams: o
            }),
            u.dispatchEvent(i)
        }, function(e) {
            L.Logger.error("Not Connected! Error: " + e),
            e = FmVideoPrivate.RoomEvent({
                type: "room-error",
                message: e
            }),
            u.dispatchEvent(e)
        })
    }
    ,
    u.disconnect = function() {
        L.Logger.debug("Disconnection requested");
        var e = FmVideoPrivate.RoomEvent({
            type: "room-disconnected",
            message: "expected-disconnection"
        });
        u.dispatchEvent(e)
    }
    ,
    u.publish = function(n, r, i) {
        if ((r = r || {}).maxVideoBW = r.maxVideoBW || a.defaultVideoBW,
        r.maxVideoBW > a.maxVideoBW && (r.maxVideoBW = a.maxVideoBW),
        void 0 === r.minVideoBW && (r.minVideoBW = 0),
        r.minVideoBW > a.defaultVideoBW && (r.minVideoBW = a.defaultVideoBW),
        n.local && void 0 === u.localStreams[n.getID()])
            if (n.hasAudio() || n.hasVideo() || n.hasScreen())
                if (void 0 !== n.url || void 0 !== n.recording) {
                    var e, t;
                    n.url ? (e = "url",
                    t = n.url) : (e = "recording",
                    t = n.recording),
                    L.Logger.info("Checking publish options for", n.getID()),
                    n.checkOptions(r),
                    o("publish", {
                        state: e,
                        data: n.hasData(),
                        audio: n.hasAudio(),
                        video: n.hasVideo(),
                        attributes: n.getAttributes(),
                        createOffer: r.createOffer
                    }, t, function(e, t) {
                        null !== e ? (L.Logger.info("Stream published"),
                        n.getID = function() {
                            return e
                        }
                        ,
                        n.sendData = function(e) {
                            s(n, e)
                        }
                        ,
                        n.setAttributes = function(e) {
                            c(n, e)
                        }
                        ,
                        (u.localStreams[e] = n).room = u,
                        i && i(e)) : (L.Logger.error("Error when publishing stream", t),
                        i && i(void 0, t))
                    })
                } else
                    u.p2p ? (a.maxAudioBW = r.maxAudioBW,
                    a.maxVideoBW = r.maxVideoBW,
                    o("publish", {
                        state: "p2p",
                        data: n.hasData(),
                        audio: n.hasAudio(),
                        video: n.hasVideo(),
                        screen: n.hasScreen(),
                        attributes: n.getAttributes()
                    }, void 0, function(e, t) {
                        null === e && (L.Logger.error("Error when publishing the stream", t),
                        i && i(void 0, t)),
                        L.Logger.info("Stream published"),
                        n.getID = function() {
                            return e
                        }
                        ,
                        n.hasData() && (n.sendData = function(e) {
                            s(n, e)
                        }
                        ),
                        n.setAttributes = function(e) {
                            c(n, e)
                        }
                        ,
                        (u.localStreams[e] = n).room = u
                    })) : (L.Logger.info("Publishing to FmVideoPrivate Normally, is createOffer", r.createOffer),
                    o("publish", {
                        state: "erizo",
                        data: n.hasData(),
                        audio: n.hasAudio(),
                        video: n.hasVideo(),
                        screen: n.hasScreen(),
                        minVideoBW: r.minVideoBW,
                        attributes: n.getAttributes(),
                        createOffer: r.createOffer,
                        scheme: r.scheme
                    }, void 0, function(e, t) {
                        null === e ? (L.Logger.error("Error when publishing the stream: ", t),
                        i && i(void 0, t)) : (L.Logger.info("Stream assigned an Id, starting the publish process"),
                        n.getID = function() {
                            return e
                        }
                        ,
                        n.hasData() && (n.sendData = function(e) {
                            s(n, e)
                        }
                        ),
                        n.setAttributes = function(e) {
                            c(n, e)
                        }
                        ,
                        (u.localStreams[e] = n).room = u,
                        n.pc = FmVideoPrivate.Connection({
                            callback: function(e) {
                                L.Logger.debug("Sending message", e),
                                o("signaling_message", {
                                    streamId: n.getID(),
                                    msg: e
                                }, void 0, function() {})
                            },
                            iceServers: u.iceServers,
                            maxAudioBW: r.maxAudioBW,
                            maxVideoBW: r.maxVideoBW,
                            limitMaxAudioBW: a.maxAudioBW,
                            limitMaxVideoBW: a.maxVideoBW,
                            audio: n.hasAudio(),
                            video: n.hasVideo()
                        }),
                        n.pc.addStream(n.stream),
                        n.pc.oniceconnectionstatechange = function(e) {
                            "failed" !== e || 0 === u.state || n.failed || (L.Logger.warning("Stream", n.getID(), "has failed after succesful ICE checks"),
                            e = FmVideoPrivate.StreamEvent({
                                type: "stream-failed",
                                msg: "Publishing stream failed after connection",
                                stream: n
                            }),
                            u.dispatchEvent(e),
                            u.unpublish(n))
                        }
                        ,
                        r.createOffer || n.pc.createOffer(),
                        i && i(e))
                    }));
            else
                n.hasData() && o("publish", {
                    state: "data",
                    data: n.hasData(),
                    audio: !1,
                    video: !1,
                    screen: !1,
                    attributes: n.getAttributes()
                }, void 0, function(e, t) {
                    null === e ? (L.Logger.error("Error publishing stream ", t),
                    i && i(void 0, t)) : (L.Logger.info("Stream published"),
                    n.getID = function() {
                        return e
                    }
                    ,
                    n.sendData = function(e) {
                        s(n, e)
                    }
                    ,
                    n.setAttributes = function(e) {
                        c(n, e)
                    }
                    ,
                    (u.localStreams[e] = n).room = u,
                    i && i(e))
                })
    }
    ,
    u.startRecording = function(e, n) {
        L.Logger.debug("Start Recording stream: " + e.getID()),
        i("startRecorder", {
            to: e.getID()
        }, function(e, t) {
            null === e ? (L.Logger.error("Error on start recording", t),
            n && n(void 0, t)) : (L.Logger.info("Start recording", e),
            n && n(e))
        })
    }
    ,
    u.stopRecording = function(n, r) {
        i("stopRecorder", {
            id: n
        }, function(e, t) {
            null === e ? (L.Logger.error("Error on stop recording", t),
            r && r(void 0, t)) : (L.Logger.info("Stop recording", n),
            r && r(!0))
        })
    }
    ,
    u.unpublish = function(e, n) {
        if (e.local) {
            i("unpublish", e.getID(), function(e, t) {
                null === e ? (L.Logger.error("Error unpublishing stream", t),
                n && n(void 0, t)) : (L.Logger.info("Stream unpublished"),
                n && n(!0))
            });
            var t = e.room.p2p;
            if (e.room = void 0,
            (e.hasAudio() || e.hasVideo() || e.hasScreen()) && void 0 === e.url)
                if (t)
                    for (var r in e.pc)
                        e.pc[r].close(),
                        e.pc[r] = void 0;
                else
                    e.pc.close(),
                    e.pc = void 0;
            delete u.localStreams[e.getID()],
            e.getID = function() {}
            ,
            e.sendData = function() {}
            ,
            e.setAttributes = function() {}
        } else
            L.Logger.error("Cannot unpublish, stream does not exist or is not local"),
            n && n(void 0, error)
    }
    ,
    u.subscribe = function(r, n, i) {
        if (n = n || {},
        !r.local) {
            if (r.hasVideo() || r.hasAudio() || r.hasScreen())
                u.p2p ? (o("subscribe", {
                    streamId: r.getID()
                }),
                i && i(!0)) : (L.Logger.info("Checking subscribe options for", r.getID()),
                r.checkOptions(n),
                o("subscribe", {
                    streamId: r.getID(),
                    audio: n.audio,
                    video: n.video,
                    data: n.data,
                    browser: FmVideoPrivate.getBrowser(),
                    createOffer: n.createOffer,
                    slideShowMode: n.slideShowMode
                }, void 0, function(e, t) {
                    null === e ? (L.Logger.error("Error subscribing to stream ", t),
                    i && i(void 0, t)) : (L.Logger.info("Subscriber added"),
                    r.pc = FmVideoPrivate.Connection({
                        callback: function(e) {
                            L.Logger.info("Sending message", e),
                            o("signaling_message", {
                                streamId: r.getID(),
                                msg: e,
                                browser: r.pc.browser
                            }, void 0, function() {})
                        },
                        nop2p: !0,
                        audio: n.audio,
                        video: n.video,
                        iceServers: u.iceServers
                    }),
                    r.pc.onaddstream = function(e) {
                        L.Logger.info("Stream subscribed"),
                        r.stream = e.stream,
                        e = FmVideoPrivate.StreamEvent({
                            type: "stream-subscribed",
                            stream: r
                        }),
                        u.dispatchEvent(e)
                    }
                    ,
                    r.pc.oniceconnectionstatechange = function(e) {
                        "failed" === e && 0 !== u.state && (e = FmVideoPrivate.StreamEvent({
                            type: "stream-failed",
                            msg: "Subscribing stream failed after connection",
                            stream: r
                        }),
                        u.dispatchEvent(e))
                    }
                    ,
                    r.pc.createOffer(!0),
                    i && i(!0))
                }));
            else {
                if (!r.hasData() || !1 === n.data)
                    return void L.Logger.info("Subscribing to anything");
                o("subscribe", {
                    streamId: r.getID(),
                    data: n.data
                }, void 0, function(e, t) {
                    if (null === e)
                        L.Logger.error("Error subscribing to stream ", t),
                        i && i(void 0, t);
                    else {
                        L.Logger.info("Stream subscribed");
                        var n = FmVideoPrivate.StreamEvent({
                            type: "stream-subscribed",
                            stream: r
                        });
                        u.dispatchEvent(n),
                        i && i(!0)
                    }
                })
            }
            L.Logger.info("Subscribing to: " + r.getID())
        }
    }
    ,
    u.unsubscribe = function(n, r) {
        void 0 !== u.socket && (n.local || i("unsubscribe", n.getID(), function(e, t) {
            null === e ? r && r(void 0, t) : (d(n),
            r && r(!0))
        }, function() {
            L.Logger.error("Error calling unsubscribe.")
        }))
    }
    ,
    u.getStreamsByAttribute = function(e, t) {
        var n, r, i = [];
        for (n in u.remoteStreams)
            u.remoteStreams.hasOwnProperty(n) && (void 0 !== (r = u.remoteStreams[n]).getAttributes() && void 0 !== r.getAttributes()[e] && r.getAttributes()[e] === t && i.push(r));
        return i
    }
    ,
    u
}
;
var L = L || {};
L.Logger = function(i) {
    return {
        DEBUG: 0,
        TRACE: 1,
        INFO: 2,
        WARNING: 3,
        ERROR: 4,
        NONE: 5,
        enableLogPanel: function() {
            i.Logger.panel = document.createElement("textarea"),
            i.Logger.panel.setAttribute("id", "licode-logs"),
            i.Logger.panel.setAttribute("style", "width: 100%; height: 100%; display: none"),
            i.Logger.panel.setAttribute("rows", 20),
            i.Logger.panel.setAttribute("cols", 20),
            i.Logger.panel.setAttribute("readOnly", !0),
            document.body.appendChild(i.Logger.panel)
        },
        setLogLevel: function(e) {
            e > i.Logger.NONE ? e = i.Logger.NONE : e < i.Logger.DEBUG && (e = i.Logger.DEBUG),
            i.Logger.logLevel = e
        },
        log: function(e) {
            var t = "";
            if (!(e < i.Logger.logLevel)) {
                e === i.Logger.DEBUG ? t += "DEBUG" : e === i.Logger.TRACE ? t += "TRACE" : e === i.Logger.INFO ? t += "INFO" : e === i.Logger.WARNING ? t += "WARNING" : e === i.Logger.ERROR && (t += "ERROR");
                t = t + ": ";
                for (var n = [], r = 0; r < arguments.length; r++)
                    n[r] = arguments[r];
                if (n = n.slice(1),
                n = [t].concat(n),
                void 0 !== i.Logger.panel) {
                    for (t = "",
                    r = 0; r < n.length; r++)
                        t += n[r];
                    i.Logger.panel.value = i.Logger.panel.value + "\n" + t
                } else
                    console.log.apply(console, n)
            }
        },
        debug: function() {
            for (var e = [], t = 0; t < arguments.length; t++)
                e[t] = arguments[t];
            i.Logger.log.apply(i.Logger, [i.Logger.DEBUG].concat(e))
        },
        trace: function() {
            for (var e = [], t = 0; t < arguments.length; t++)
                e[t] = arguments[t];
            i.Logger.log.apply(i.Logger, [i.Logger.TRACE].concat(e))
        },
        info: function() {
            for (var e = [], t = 0; t < arguments.length; t++)
                e[t] = arguments[t];
            i.Logger.log.apply(i.Logger, [i.Logger.INFO].concat(e))
        },
        warning: function() {
            for (var e = [], t = 0; t < arguments.length; t++)
                e[t] = arguments[t];
            i.Logger.log.apply(i.Logger, [i.Logger.WARNING].concat(e))
        },
        error: function() {
            for (var e = [], t = 0; t < arguments.length; t++)
                e[t] = arguments[t];
            i.Logger.log.apply(i.Logger, [i.Logger.ERROR].concat(e))
        }
    }
}(L),
L = L || {},
L.Base64 = function() {
    var i, t, n, r, e, o, a, s, c;
    for (i = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,0,1,2,3,4,5,6,7,8,9,+,/".split(","),
    t = [],
    e = 0; e < i.length; e += 1)
        t[i[e]] = e;
    return o = function(e) {
        n = e,
        r = 0
    }
    ,
    a = function() {
        var e;
        return !n || r >= n.length ? -1 : (e = 255 & n.charCodeAt(r),
        r += 1,
        e)
    }
    ,
    s = function() {
        if (!n)
            return -1;
        for (; ; ) {
            if (r >= n.length)
                return -1;
            var e = n.charAt(r);
            if (r += 1,
            t[e])
                return t[e];
            if ("A" === e)
                return 0
        }
    }
    ,
    c = function(e) {
        return 1 === (e = e.toString(16)).length && (e = "0" + e),
        unescape("%" + e)
    }
    ,
    {
        encodeBase64: function(e) {
            var t, n, r;
            for (o(e),
            e = "",
            t = Array(3),
            n = 0,
            r = !1; !r && -1 !== (t[0] = a()); )
                t[1] = a(),
                t[2] = a(),
                e += i[t[0] >> 2],
                -1 !== t[1] ? (e += i[t[0] << 4 & 48 | t[1] >> 4],
                -1 !== t[2] ? (e += i[t[1] << 2 & 60 | t[2] >> 6],
                e += i[63 & t[2]]) : (e += i[t[1] << 2 & 60],
                e += "=",
                r = !0)) : (e += i[t[0] << 4 & 48],
                e += "=",
                e += "=",
                r = !0),
                76 <= (n += 4) && (e += "\n",
                n = 0);
            return e
        },
        decodeBase64: function(e) {
            var t, n;
            for (o(e),
            e = "",
            t = Array(4),
            n = !1; !n && -1 !== (t[0] = s()) && -1 !== (t[1] = s()); )
                t[2] = s(),
                t[3] = s(),
                e += c(t[0] << 2 & 255 | t[1] >> 4),
                -1 !== t[2] ? (e += c(t[1] << 4 & 255 | t[2] >> 2),
                -1 !== t[3] ? e += c(t[2] << 6 & 255 | t[3]) : n = !0) : n = !0;
            return e
        }
    }
}(),
function() {
    function e() {
        (new L.ElementQueries).init()
    }
    this.L = this.L || {},
    this.L.ElementQueries = function() {
        function r(e) {
            return e || (e = document.documentElement),
            e = getComputedStyle(e, "fontSize"),
            parseFloat(e) || 16
        }
        function l(e, t) {
            var n = t.replace(/[0-9]*/, "");
            t = parseFloat(t);
            switch (n) {
            case "px":
                return t;
            case "em":
                return t * r(e);
            case "rem":
                return t * r();
            case "vw":
                return t * document.documentElement.clientWidth / 100;
            case "vh":
                return t * document.documentElement.clientHeight / 100;
            case "vmin":
            case "vmax":
                return t * (0,
                Math["vmin" === n ? "min" : "max"])(document.documentElement.clientWidth / 100, document.documentElement.clientHeight / 100);
            default:
                return t
            }
        }
        function n(e) {
            this.element = e,
            this.options = [];
            var t, n, r, i, o, a, s, c, d = 0, u = 0;
            this.addOption = function(e) {
                this.options.push(e)
            }
            ;
            var p = ["min-width", "min-height", "max-width", "max-height"];
            this.call = function() {
                for (d = this.element.offsetWidth,
                u = this.element.offsetHeight,
                a = {},
                t = 0,
                n = this.options.length; t < n; t++)
                    r = this.options[t],
                    i = l(this.element, r.value),
                    o = "width" == r.property ? d : u,
                    c = r.mode + "-" + r.property,
                    s = "",
                    "min" == r.mode && i <= o && (s += r.value),
                    "max" == r.mode && o <= i && (s += r.value),
                    a[c] || (a[c] = ""),
                    s && -1 === (" " + a[c] + " ").indexOf(" " + s + " ") && (a[c] += " " + s);
                for (var e in p)
                    a[p[e]] ? this.element.setAttribute(p[e], a[p[e]].substr(1)) : this.element.removeAttribute(p[e])
            }
        }
        function s(e, t) {
            e.elementQueriesSetupInformation ? e.elementQueriesSetupInformation.addOption(t) : (e.elementQueriesSetupInformation = new n(e),
            e.elementQueriesSetupInformation.addOption(t),
            new ResizeSensor(e,function() {
                e.elementQueriesSetupInformation.call()
            }
            )),
            e.elementQueriesSetupInformation.call()
        }
        function i(e) {
            var t;
            for (e = e.replace(/'/g, '"'); null !== (t = c.exec(e)); )
                if (5 < t.length) {
                    var n = t[1] || t[5]
                      , r = t[2]
                      , i = t[3];
                    t = t[4];
                    var o = void 0;
                    if (document.querySelectorAll && (o = document.querySelectorAll.bind(document)),
                    !o && "undefined" != typeof $$ && (o = $$),
                    !o && "undefined" != typeof jQuery && (o = jQuery),
                    !o)
                        throw "No document.querySelectorAll, jQuery or Mootools's $$ found.";
                    n = o(n),
                    o = 0;
                    for (var a = n.length; o < a; o++)
                        s(n[o], {
                            mode: r,
                            property: i,
                            value: t
                        })
                }
        }
        function o(e) {
            var t = "";
            if (e)
                if ("string" == typeof e)
                    (-1 !== (e = e.toLowerCase()).indexOf("min-width") || -1 !== e.indexOf("max-width")) && i(e);
                else
                    for (var n = 0, r = e.length; n < r; n++)
                        1 === e[n].type ? -1 !== (t = e[n].selectorText || e[n].cssText).indexOf("min-height") || -1 !== t.indexOf("max-height") ? i(t) : (-1 !== t.indexOf("min-width") || -1 !== t.indexOf("max-width")) && i(t) : 4 === e[n].type && o(e[n].cssRules || e[n].rules)
        }
        var c = /,?([^,\n]*)\[[\s\t]*(min|max)-(width|height)[\s\t]*[~$\^]?=[\s\t]*"([^"]*)"[\s\t]*]([^\n\s\{]*)/gim;
        this.init = function() {
            for (var e = 0, t = document.styleSheets.length; e < t; e++)
                o(document.styleSheets[e].cssText || document.styleSheets[e].cssRules || document.styleSheets[e].rules)
        }
    }
    ,
    window.addEventListener ? window.addEventListener("load", e, !1) : window.attachEvent("onload", e),
    this.L.ResizeSensor = function(e, t) {
        function c(e, t) {
            window.OverflowEvent ? e.addEventListener("overflowchanged", function(e) {
                t.call(this, e)
            }) : (e.addEventListener("overflow", function(e) {
                t.call(this, e)
            }),
            e.addEventListener("underflow", function(e) {
                t.call(this, e)
            }))
        }
        function d() {
            var e, t;
            this.q = [],
            this.add = function(e) {
                this.q.push(e)
            }
            ,
            this.call = function() {
                for (e = 0,
                t = this.q.length; e < t; e++)
                    this.q[e].call()
            }
        }
        function n(r, e) {
            function t() {
                var e = !1
                  , t = r.resizeSensor.offsetWidth
                  , n = r.resizeSensor.offsetHeight;
                return i != t && (a.width = t - 1 + "px",
                s.width = t + 1 + "px",
                e = !0,
                i = t),
                o != n && (a.height = n - 1 + "px",
                s.height = n + 1 + "px",
                e = !0,
                o = n),
                e
            }
            if (r.resizedAttached) {
                if (r.resizedAttached)
                    return void r.resizedAttached.add(e)
            } else
                r.resizedAttached = new d,
                r.resizedAttached.add(e);
            var n = function() {
                t() && r.resizedAttached.call()
            };
            r.resizeSensor = document.createElement("div"),
            r.resizeSensor.className = "resize-sensor",
            r.resizeSensor.style.cssText = "position: absolute; left: 0; top: 0; right: 0; bottom: 0; overflow: hidden; z-index: -1;",
            r.resizeSensor.innerHTML = '<div class="resize-sensor-overflow" style="position: absolute; left: 0; top: 0; right: 0; bottom: 0; overflow: hidden; z-index: -1;"><div></div></div><div class="resize-sensor-underflow" style="position: absolute; left: 0; top: 0; right: 0; bottom: 0; overflow: hidden; z-index: -1;"><div></div></div>',
            r.appendChild(r.resizeSensor),
            "absolute" !== (r.currentStyle ? r.currentStyle.position : window.getComputedStyle ? window.getComputedStyle(r, null).getPropertyValue("position") : r.style.position) && (r.style.position = "relative");
            var i = -1
              , o = -1
              , a = r.resizeSensor.firstElementChild.firstChild.style
              , s = r.resizeSensor.lastElementChild.firstChild.style;
            t(),
            c(r.resizeSensor, n),
            c(r.resizeSensor.firstElementChild, n),
            c(r.resizeSensor.lastElementChild, n)
        }
        if ("array" == typeof e || "undefined" != typeof jQuery && e instanceof jQuery || "undefined" != typeof Elements && e instanceof Elements)
            for (var r = 0, i = e.length; r < i; r++)
                n(e[r], t);
        else
            n(e, t)
    }
}(),
FmVideoPrivate = FmVideoPrivate || {},
FmVideoPrivate.View = function() {
    var e = FmVideoPrivate.EventDispatcher({});
    return e.url = "",
    e
}
,
FmVideoPrivate = FmVideoPrivate || {},
FmVideoPrivate.VideoPlayer = function(n) {
    var r = FmVideoPrivate.View({});
    return r.id = n.id,
    r.stream = n.stream.stream,
    r.elementID = n.elementID,
    r.destroy = function() {
        r.video.pause(),
        delete r.resizer,
        r.parentNode.removeChild(r.div)
    }
    ,
    r.resize = function() {
        var e = r.container.offsetWidth
          , t = r.container.offsetHeight;
        n.stream.screen || !1 === n.options.crop ? .5625 * e < t ? (r.video.style.width = e + "px",
        r.video.style.height = .5625 * e + "px",
        r.video.style.top = -(.5625 * e / 2 - t / 2) + "px",
        r.video.style.left = "0px") : (r.video.style.height = t + "px",
        r.video.style.width = 16 / 9 * t + "px",
        r.video.style.left = -(16 / 9 * t / 2 - e / 2) + "px",
        r.video.style.top = "0px") : e === r.containerWidth && t === r.containerHeight || (t < .75 * e ? (r.video.style.width = e + "px",
        r.video.style.height = .75 * e + "px",
        r.video.style.top = -(.75 * e / 2 - t / 2) + "px",
        r.video.style.left = "0px") : (r.video.style.height = t + "px",
        r.video.style.width = 4 / 3 * t + "px",
        r.video.style.left = -(4 / 3 * t / 2 - e / 2) + "px",
        r.video.style.top = "0px")),
        r.containerWidth = e,
        r.containerHeight = t
    }
    ,
    L.Logger.debug("Creating URL from stream " + r.stream),
    r.stream_url = (window.URL || webkitURL).createObjectURL(r.stream),
    r.div = document.createElement("div"),
    r.div.setAttribute("id", "player_" + r.id),
    r.div.setAttribute("class", "player"),
    r.div.setAttribute("style", "width: 100%; height: 100%; position: relative; background-color: black; overflow: hidden;"),
    r.loader = document.createElement("img"),
    r.loader.setAttribute("style", "width: 16px; height: 16px; position: absolute; top: 50%; left: 50%; margin-top: -8px; margin-left: -8px"),
    r.loader.setAttribute("id", "back_" + r.id),
    r.loader.setAttribute("class", "loader"),
    r.loader.setAttribute("src", r.url + "assets/loader.gif"),
    r.video = document.createElement("video"),
    r.video.setAttribute("id", "stream" + r.id),
    r.video.setAttribute("class", "stream"),
    r.video.setAttribute("style", "width: 100%; height: 100%; position: absolute"),
    r.video.setAttribute("autoplay", "autoplay"),
    n.stream.local && (r.video.volume = 0),
    void 0 !== r.elementID ? (document.getElementById(r.elementID).appendChild(r.div),
    r.container = document.getElementById(r.elementID)) : (document.body.appendChild(r.div),
    r.container = document.body),
    r.parentNode = r.div.parentNode,
    r.div.appendChild(r.loader),
    r.div.appendChild(r.video),
    r.containerWidth = 0,
    r.containerHeight = 0,
    r.resizer = new L.ResizeSensor(r.container,r.resize),
    r.resize(),
    r.div.onmouseover = function() {}
    ,
    r.div.onmouseout = function() {}
    ,
    r.video.src = r.stream_url,
    r
}
,
FmVideoPrivate = FmVideoPrivate || {},
FmVideoPrivate.AudioPlayer = function(e) {
    var t, n, r = FmVideoPrivate.View({});
    return r.id = e.id,
    r.stream = e.stream.stream,
    r.elementID = e.elementID,
    L.Logger.debug("Creating URL from stream " + r.stream),
    r.stream_url = (window.URL || webkitURL).createObjectURL(r.stream),
    r.audio = document.createElement("audio"),
    r.audio.setAttribute("id", "stream" + r.id),
    r.audio.setAttribute("class", "stream"),
    r.audio.setAttribute("style", "width: 100%; height: 100%; position: absolute"),
    r.audio.setAttribute("autoplay", "autoplay"),
    e.stream.local && (r.audio.volume = 0),
    e.stream.local && (r.audio.volume = 0),
    void 0 !== r.elementID ? (r.destroy = function() {
        r.audio.pause(),
        r.parentNode.removeChild(r.div)
    }
    ,
    t = function() {
        r.bar.display()
    }
    ,
    n = function() {
        r.bar.hide()
    }
    ,
    r.div = document.createElement("div"),
    r.div.setAttribute("id", "player_" + r.id),
    r.div.setAttribute("class", "player"),
    r.div.setAttribute("style", "width: 100%; height: 100%; position: relative; overflow: hidden;"),
    document.getElementById(r.elementID).appendChild(r.div),
    r.container = document.getElementById(r.elementID),
    r.parentNode = r.div.parentNode,
    r.div.appendChild(r.audio),
    r.bar = new FmVideoPrivate.Bar({
        elementID: "player_" + r.id,
        id: r.id,
        stream: e.stream,
        media: r.audio,
        options: e.options
    }),
    r.div.onmouseover = t,
    r.div.onmouseout = n) : (r.destroy = function() {
        r.audio.pause(),
        r.parentNode.removeChild(r.audio)
    }
    ,
    document.body.appendChild(r.audio),
    r.parentNode = document.body),
    r.audio.src = r.stream_url,
    r
}
,
FmVideoPrivate = FmVideoPrivate || {},
FmVideoPrivate.Bar = function(e) {
    var t, n, r = FmVideoPrivate.View({});
    return r.elementID = e.elementID,
    r.id = e.id,
    r.div = document.createElement("div"),
    r.div.setAttribute("id", "bar_" + r.id),
    r.div.setAttribute("class", "bar"),
    r.bar = document.createElement("div"),
    r.bar.setAttribute("style", "width: 100%; height: 15%; max-height: 30px; position: absolute; bottom: 0; right: 0; background-color: rgba(255,255,255,0.62)"),
    r.bar.setAttribute("id", "subbar_" + r.id),
    r.bar.setAttribute("class", "subbar"),
    r.link = document.createElement("a"),
    r.link.setAttribute("href", "http://www.fm.com/"),
    r.link.setAttribute("class", "link"),
    r.link.setAttribute("target", "_blank"),
    r.logo = document.createElement("img"),
    r.logo.setAttribute("style", "width: 100%; height: 100%; max-width: 30px; position: absolute; top: 0; left: 2px;"),
    r.logo.setAttribute("class", "logo"),
    r.logo.setAttribute("alt", "Fm"),
    r.logo.setAttribute("src", r.url + "/assets/star.svg"),
    n = function(e) {
        "block" !== e ? e = "none" : clearTimeout(t),
        r.div.setAttribute("style", "width: 100%; height: 100%; position: relative; bottom: 0; right: 0; display:" + e)
    }
    ,
    r.display = function() {
        n("block")
    }
    ,
    r.hide = function() {
        t = setTimeout(n, 1e3)
    }
    ,
    document.getElementById(r.elementID).appendChild(r.div),
    r.link.appendChild(r.logo),
    e.stream.screen || void 0 !== e.options && void 0 !== e.options.speaker && !0 !== e.options.speaker || (r.speaker = new FmVideoPrivate.Speaker({
        elementID: "subbar_" + r.id,
        id: r.id,
        stream: e.stream,
        media: e.media
    })),
    r.display(),
    r.hide(),
    r
}
,
FmVideoPrivate = FmVideoPrivate || {},
FmVideoPrivate.Speaker = function(e) {
    var t, n, r, i = FmVideoPrivate.View({}), o = 50;
    return i.elementID = e.elementID,
    i.media = e.media,
    i.id = e.id,
    i.stream = e.stream,
    i.div = document.createElement("div"),
    i.div.setAttribute("style", "width: 40%; height: 100%; max-width: 32px; position: absolute; right: 0;z-index:0;"),
    i.icon = document.createElement("img"),
    i.icon.setAttribute("id", "volume_" + i.id),
    i.icon.setAttribute("src", i.url + "/assets/sound48.png"),
    i.icon.setAttribute("style", "width: 80%; height: 100%; position: absolute;"),
    i.div.appendChild(i.icon),
    i.stream.local ? (n = function() {
        i.media.muted = !0,
        i.icon.setAttribute("src", i.url + "/assets/mute48.png"),
        i.stream.stream.getAudioTracks()[0].enabled = !1
    }
    ,
    r = function() {
        i.media.muted = !1,
        i.icon.setAttribute("src", i.url + "/assets/sound48.png"),
        i.stream.stream.getAudioTracks()[0].enabled = !0
    }
    ,
    i.icon.onclick = function() {
        i.media.muted ? r() : n()
    }
    ) : (i.picker = document.createElement("input"),
    i.picker.setAttribute("id", "picker_" + i.id),
    i.picker.type = "range",
    i.picker.min = 0,
    i.picker.max = 100,
    i.picker.step = 10,
    i.picker.value = o,
    i.picker.setAttribute("orient", "vertical"),
    i.div.appendChild(i.picker),
    i.media.volume = i.picker.value / 100,
    i.media.muted = !1,
    i.picker.oninput = function() {
        0 < i.picker.value ? (i.media.muted = !1,
        i.icon.setAttribute("src", i.url + "/assets/sound48.png")) : (i.media.muted = !0,
        i.icon.setAttribute("src", i.url + "/assets/mute48.png")),
        i.media.volume = i.picker.value / 100
    }
    ,
    t = function(e) {
        i.picker.setAttribute("style", "background: transparent; width: 32px; height: 100px; position: absolute; bottom: 90%; z-index: 1;" + i.div.offsetHeight + "px; right: 0px; -webkit-appearance: slider-vertical; display: " + e)
    }
    ,
    n = function() {
        i.icon.setAttribute("src", i.url + "/assets/mute48.png"),
        o = i.picker.value,
        i.picker.value = 0,
        i.media.volume = 0,
        i.media.muted = !0
    }
    ,
    r = function() {
        i.icon.setAttribute("src", i.url + "/assets/sound48.png"),
        i.picker.value = o,
        i.media.volume = i.picker.value / 100,
        i.media.muted = !1
    }
    ,
    i.icon.onclick = function() {
        i.media.muted ? r() : n()
    }
    ,
    i.div.onmouseover = function() {
        t("block")
    }
    ,
    i.div.onmouseout = function() {
        t("none")
    }
    ,
    t("none")),
    document.getElementById(i.elementID).appendChild(i.div),
    i
}
;
var Stringee = Stringee || {};
Stringee.VideoConference = {
    room: null
},
Stringee.VideoConference.createLocalStream = function(e, t, n) {
    e.attributes = {
        userInfo: Stringee.userInfo
    };
    var r = FmVideoPrivate.Stream(e);
    return r.addEventListener("access-accepted", function() {
        t.call()
    }),
    r.addEventListener("access-denied", function() {
        n.call()
    }),
    r.init(),
    r
}
,
Stringee.VideoConference.mute = function(e, t) {
    var n = e.stream.getAudioTracks();
    0 < n.length && (n[0].enabled = !t)
}
,
Stringee.VideoConference.enableVideo = function(e, t) {
    var n = e.stream.getVideoTracks();
    0 < n.length && (n[0].enabled = t)
}
,
Stringee.VideoConference.roomDisconnected = function(e) {}
,
Stringee.VideoConference.roomStreamSubscribed = function(e) {}
,
Stringee.VideoConference.roomStreamAdded = function(e) {}
,
Stringee.VideoConference.roomStreamRemoved = function(e) {}
,
Stringee.VideoConference.connectRoom = function(e, t, n, r) {
    L.Logger.setLogLevel(L.Logger.ERROR),
    this.room = FmVideoPrivate.Room({
        token: t
    }),
    this.room.stringeeRoomId = e,
    this.room.addEventListener("room-connected", function(e) {
        n(e)
    }),
    this.room.addEventListener("room-error", function(e) {
        r(e)
    }),
    this.room.addEventListener("room-disconnected", function(e) {
        Stringee.VideoConference.roomDisconnected(e)
    }),
    this.room.addEventListener("stream-subscribed", function(e) {
        Stringee.VideoConference.roomStreamSubscribed(e)
    }),
    this.room.addEventListener("stream-added", function(e) {
        var t = {
            roomId: Stringee.VideoConference.room.stringeeRoomId,
            streamId: e.stream.getID(),
            attributes: e.stream.getAttributes()
        };
        Stringee.sendRoomStreamAdded(t, function(e) {}),
        Stringee.VideoConference.roomStreamAdded(e)
    }),
    this.room.addEventListener("stream-removed", function(e) {
        var t = {
            roomId: Stringee.VideoConference.room.stringeeRoomId,
            streamId: e.stream.getID(),
            attributes: e.stream.getAttributes()
        };
        Stringee.sendRoomStreamRemoved(t, function(e) {}),
        Stringee.VideoConference.roomStreamRemoved(e)
    }),
    this.room.addEventListener("stream-failed", function(e) {
        console.log("Stream Failed, act accordingly")
    }),
    this.room.connect()
}
,
Stringee.VideoConference.disconnectRoom = function() {
    this.room && this.room.disconnect()
}
,
Stringee.VideoConference.publishLocalStream = function(e, t) {
    e && this.room && this.room.publish(e, t)
}
,
Stringee.VideoConference.unpublishLocalStream = function(e) {
    e && this.room && this.room.unpublish(e)
}
,
Stringee.VideoConference.subscribeToStream = function(e, t) {
    this.room.subscribe(e, t)
}
,
Stringee.VideoConference.startRecording = function(e, n) {
    this.room.startRecording(e, function(e, t) {
        n(e, t)
    })
}
,
Stringee.VideoConference.stopRecording = function(e, n) {
    this.room.stopRecording(e, function(e, t) {
        n(e, t)
    })
}
,
Stringee.sendMakeRoom = function(e) {
    Stringee.sendMessage(SERVICE_TYPE_VIDEO_CONFERENCE_MAKE_ROOM, {}, e)
}
,
Stringee.sendJoinRoom = function(e, t) {
    var n = {
        roomId: e
    };
    Stringee.sendMessage(SERVICE_TYPE_VIDEO_CONFERENCE_JOIN_ROOM, n, t)
}
,
Stringee.sendLeaveRoom = function(e, t) {
    var n = {
        roomId: e
    };
    Stringee.sendMessage(SERVICE_TYPE_VIDEO_CONFERENCE_LEAVE_ROOM, n, t)
}
,
Stringee.sendRoomStreamAdded = function(e, t) {
    Stringee.sendMessage(SERVICE_TYPE_VIDEO_CONFERENCE_STREAM_ADDED, e, t)
}
,
Stringee.sendRoomStreamRemoved = function(e, t) {
    Stringee.sendMessage(SERVICE_TYPE_VIDEO_CONFERENCE_STREAM_REMOVED, e, t)
}
;
var Stringee = Stringee || {};
Stringee.joinRoomNotificationProcessor = function(e, t) {}
,
Stringee.leaveRoomNotificationProcessor = function(e, t) {}
;
var Stringee = Stringee || {};
Stringee.VideoConference = Stringee.VideoConference || {},
Stringee.VideoConference._processConnectRoom = function(e, n, t) {
    if (console.log(t),
    0 == t.r) {
        var r = t.roomId;
        Stringee.VideoConference.connectRoom(r, t.roomToken, function(e) {
            var t = {
                r: 0,
                reason: "room-connected",
                roomId: r,
                streams: e.streams
            };
            n(t)
        }, function(e) {
            n({
                r: 2,
                reason: "room-error"
            })
        })
    } else {
        var i, o;
        1 == e ? (console.log("res.r: " + t.r),
        1 == t.r ? o = "make-room-error" : 20 == t.r && (o = "max-rooms-reached"),
        i = {
            r: t.r,
            reason: o
        }) : 2 == e && (i = {
            r: t.r,
            reason: "join-room-error"
        }),
        n(i)
    }
}
,
Stringee.VideoConference.makeRoom = function(t) {
    Stringee.sendMakeRoom(function(e) {
        Stringee.VideoConference._processConnectRoom(1, t, e)
    })
}
,
Stringee.VideoConference.joinRoom = function(e, t) {
    Stringee.sendJoinRoom(e, function(e) {
        Stringee.VideoConference._processConnectRoom(2, t, e)
    })
}
,
Stringee.VideoConference.leaveRoom = function(e, t) {
    Stringee.sendLeaveRoom(e, function(e) {
        0 == e.r && Stringee.VideoConference.disconnectRoom(),
        t(e)
    })
}
;
