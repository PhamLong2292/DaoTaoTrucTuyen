﻿using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using OneTSQ.Core.Call.Bussiness.Utility;
using Newtonsoft.Json;
using System.Net;
using OneTSQ.Core.Model;

namespace OneTSQ.WebParts
{
    public class DT_DaoTaoTrucTuyen : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "DT_DaoTaoTrucTuyen";
            }
        }
        public static string StaticWebPartId = "DT_DaoTaoTrucTuyen";

        public override string WebPartTitle
        {
            get
            {
                return "Đào tạo trực tuyến";
            }
        }

        public override string Description
        {
            get
            {
                return "Đào tạo trực tuyến";
            }
        }
        public override string[] CheckPermissionWebPartIds
        {
            get
            {
                //return new string[] { StaticWebPartId, DT_DaoTaoTrucTuyen.StaticWebPartId };
                DT_LopHocs lopHoc = new DT_LopHocs();
                return new string[] { StaticWebPartId, lopHoc.WebPartId };
            }
        }
        public enum eCustomMsgType
        {
            giangVienServerId = 0,
            convId = 1,
        }
        public enum eChatMsgType
        {
            text = 1,
            photo = 2,
            video = 3,
            audio = 4,
            file = 5,
            link = 6,
            createGroup = 7,
            renameGroup = 8,
            location = 9,
            contact = 10,
            notify = 100//add participant
        }
        public enum eMsgReportStatus
        {
            received = 1,
            read = 2,
        }
        public struct RoomMember
        {
            public string id;
            public string ownerUserId;
            public string bacSyId;
            public string loginName;
            public string bacSyCode;
            public string bacSyFullName;
            public string donViCongTac;
        }
        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DT_DaoTaoTrucTuyen), Page);
        }
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            //3 đối tượng người dùng tham gia vào form này:
            //1. Giảng viên: user đăng nhập là một trong những user của giảng viên.
            //2. Người thuộc đơn vị đào tạo: user đăng nhập không thuộc user của giảng viên nhưng thuộc đơn vị đào tạo.
            //3. Học viên: user đăng nhập là của học viên.
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                string khoaHocId = WebEnvironments.Request("id");
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                string SessionId = System.Guid.NewGuid().ToString();
                string maDonViTuVanDefault = WebConfig.GetWebConfig("HospitalCode");
                string tokenURL = WebConfig.GetWebConfig("TokenURL");
                string answerURL = WebConfig.GetWebConfig("AnswerURL");
                string createRoomURL = WebConfig.GetWebConfig("CreateRoomURL");
                string stringeeServerAddr = WebConfig.GetWebConfig("StringeeServerAddr");
                DT_KhoaHocCls khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
                DT_LichLyThuyetChiTietCls lichLyThuyetChiTiet = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().Reading(ORenderInfo, new DT_LichLyThuyetChiTietFilterCls() { LICHLYTHUYET_ID = khoaHocId, NGAY = DateTime.Today }).Where(o => o.THOIGIAN == null || o.THOIGIAN < DateTime.Now).OrderByDescending(o => o.THOIGIAN).FirstOrDefault();
                if (lichLyThuyetChiTiet == null)
                {
                    RetAjaxOut.HtmlContent = WebScreen.GetPanelInfo(OSiteParam, WebLanguage.GetLanguage(OSiteParam, "Hôm nay không có lịch học nào."), true);
                    return RetAjaxOut;
                }
                if (string.IsNullOrEmpty(lichLyThuyetChiTiet.GIANGVIEN_ID))
                {
                    RetAjaxOut.HtmlContent = WebScreen.GetPanelInfo(OSiteParam, WebLanguage.GetLanguage(OSiteParam, "Lịch học hôm nay chưa có giảng viên."), true);
                    return RetAjaxOut;
                }
                BacSyCls giangVien = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichLyThuyetChiTiet.GIANGVIEN_ID);

                OneMES3.DM.Model.ChucDanhCls chucDanh = null;
                if (giangVien != null && !string.IsNullOrEmpty(giangVien.CHUCDANHMA))
                {
                    chucDanh = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChucDanhProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), giangVien.CHUCDANHMA);
                }

                OneMES3.DM.Model.ChuyenKhoaCls chuyenKhoa = null;
                if (giangVien != null && !string.IsNullOrEmpty(giangVien.CHUYENKHOAMA))
                {
                    chuyenKhoa = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), giangVien.CHUYENKHOAMA);                  
                }

                List<string> giangVienUserIds = CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Reading(ORenderInfo, new BacSyOwnerUserFilterCls() { BACSYID = lichLyThuyetChiTiet.GIANGVIEN_ID }).Select(o => o.OWNERUSERID).ToList();
                DT_HocVienCls[] hocViens = CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().Reading(ORenderInfo, new DT_HocVienFilterCls() { KhoaHocDuyet_Id = khoaHocId });
                DT_HocVienCls hocVien = hocViens.FirstOrDefault(o => o.USERNAME == user.LoginName);
                bool isGiangVien = false, isHocVien = false;
                //Nếu hocVien != null thì user đăng nhập là học viên
                if (hocVien != null)
                    isHocVien = true;
                if (giangVienUserIds.Contains(user.OwnerUserId))
                    isGiangVien = true;
                RetAjaxOut.HtmlContent =
                #region Javascript
                WebEnvironments.ProcessJavascript(
                               "<script src=\"../../../../themes/js/plugins/stringee/latest.sdk.bundle.min.js\" type=\"text/javascript\"></script>\r\n" +
                               " <script type=\"text/javascript\">\r\n" +
                               "    RenderInfo=CreateRenderInfo();\r\n" +
                               "    var khoaHocId = '" + khoaHoc.ID + "';\r\n" +
                               "    var stringeeClient;\r\n" +
                               "    var stringeeChat;\r\n" +
                               "    var room;\r\n" +
                               "    var roomId = '" + khoaHoc.ROOMID + "';\r\n" +
                               "    var roomToken;\r\n" +
                               "    var localTracks = [];\r\n" +
                               "    var subscribedTracks = [];\r\n" +
                               "    var trackInfos =[];\r\n" +
                               "    var mainTrackInfo;\r\n" +//Thông tin track được hiển thị ở vị trí chính cho toàn bộ room xem.
                               "    var giangVienTrack;\r\n" +//Thông tin track được hiển thị ở vị trí đại diện của giảng viên.
                               "    var giangVienTrackInfo;\r\n" +//Thông tin track info được hiển thị ở vị trí đại diện của giảng viên.
                               "    var giangVienServerId;\r\n" +//Thông tin ServerId caur track được hiển thị ở vị trí đại diện của giảng viên.
                               "    var chattoggle=true;\r\n" +
                               "    var convId = '" + khoaHoc.CONVID + "';\r\n" +
                               "    var convName;\r\n" +
                               "	var access_token;\r\n" +
                               "	var rest_access_token;\r\n" +
                               "	var room_token;\r\n" +
                               "    var isGiangVien = " + (isGiangVien ? "true" : "false") + ";\r\n" +
                               "    var isHocVien = " + (isHocVien ? "true" : "false") + ";\r\n" +
                               "    var tokenUrl = \"" + tokenURL + "\";\r\n" +
                               "    var answerUrl = \"" + answerURL + "\";\r\n" +
                               "    var createRoomURL = \"" + createRoomURL + "\";\r\n" +
                               "    var stringeeServerAddr = \"" + stringeeServerAddr + "\";\r\n" +
                               "    var isPlay = true;\r\n" +
                               "    var isUnmute = true;\r\n" +
                               "    var isShareScreen = false;\r\n" +
                               "    var readMsg;\r\n" +
                               "     $(document).ready(function () {\r\n" +
                               "       document.title = '" + WebLanguage.GetLanguage(OSiteParam, "OneTSQ: DT_DaoTaoTrucTuyen") + "';\r\n" +
                               //Set Height nội dung
                               "       leftMenuHeight = $('.sidebar-collapse').height();\r\n" +
                               "       windowHeight = $(window).height();\r\n" +
                               "            var div = document.createElement('div');\r\n" +
                               "            div.setAttribute('style', 'font-size:24px; color:#18A689; text-transform: uppercase; margin-top:10px; float:left;');\r\n" +
                               "            div.setAttribute('id', 'divConvName');\r\n" +
                               "            $('.navbar-header').append(div);\r\n" +

                               "        $(\"#txtChatContent\").keypress(function(e) {\r\n" +
                               "            if(e.keyCode == 13 && !e.shiftKey){\r\n" +
                               "                e.preventDefault();\r\n" +
                               "                SendMessage(this.value, " + (int)eChatMsgType.text + ");\r\n" +
                               "                $(\"#txtChatContent\").val(\"\");\r\n" +
                               "            }\r\n" +
                               "        });\r\n" +
                #region Ket noi den Stringee Server
                               "        stringeeClient = new StringeeClient();\r\n" +
                               "        stringeeChat = new window.DeprecatedStringeeChat(stringeeClient);\r\n" +
                               "        SettingClientEvents(stringeeClient);\r\n" +
                               "        GetAccessTokenAndConnectToStringee(stringeeClient);\r\n" +
                #endregion
                                "    });\r\n" +

                #region Gửi SCCO tới stringee server thông qua answerurl
                                "   $.ajax({ \r\n" +
                                "       type : 'GET', \r\n" +
                                "       url : answerUrl, \r\n" +
                                "       success : function(data, textStatus, xhr) { \r\n" +
                                "       console.log(data);\r\n" +
                                "       },\r\n" +
                                "       error : function(data, textStatus, xhr) { \r\n" +
                                "       } \r\n" +
                                "   }); \r\n" +
                #endregion

                               "     $(window).resize(function () {\r\n" +
                               "       leftMenuHeight = $('.sidebar-collapse').height();\r\n" +
                               "       windowHeight = $(window).height();\r\n" +
                               //"       $('.chat-discussion').css('height', '200px');\r\n" +
                               //"       $('#divContent').height(windowHeight - 60); \r\n" +
                               "    });\r\n" +
                #region Video DT_DaoTaoTrucTuyen
                #region Các sự kiện video DT_DaoTaoTrucTuyen
                #region Các sự kiện stringee client
                               "    function SettingClientEvents(client){\r\n" +
                #region Các sự kiện kết nối stringee
                               "        client.on('connect', function () {\r\n" +
                               "            console.log('connected');\r\n" +
                               "        });\r\n" +

                               "        client.on('authen', function (res) {\r\n" +
                               "            console.log('authen', res);\r\n" +
                               "            $('#loggedUserId').html(res.userId);\r\n" +
                               //Nếu chưa  join room thì thực hiện join room
                               "            if(room == undefined)\r\n" +
                               "                JoinRoom(false);\r\n" +
                               "            if(convId != ''){\r\n" +
                               "                AddParticipant(['" + user.LoginName + "']);\r\n" +
                               //"                LoadMsgs(0, Number.MAX_VALUE);\r\n" +
                               "            }\r\n" +
                               "            console.log('authen', res);\r\n" +
                               "        });\r\n" +

                               "        client.on('disconnect', function () {\r\n" +
                               "            console.log('disconnected');\r\n" +
                               "        });\r\n" +

                               "        client.on('requestnewtoken', function () {\r\n" +
                               "            console.log('++++++++++++++ requestnewtoken; please get new access_token from YourServer and call client.connect(new_access_token)+++++++++');\r\n" +
                               "            //please get new access_token from YourServer and call: \r\n" +
                               "            GetAccessTokenAndConnectToStringee(client);\r\n" +
                               "        });\r\n" +
                #endregion
                #region Các sự kiện chat
                                //Sự kiện được kích hoạt khi có tin nhắn được gửi đi
                                "        client.on('chatmessage', function (msg) {\r\n" +
                                "            console.log('chatmessage', msg);\r\n" +
                                "            if(room == '')return;\r\n" +//trường hợp chưa có video DT_DaoTaoTrucTuyen thì ko nhận chat
                                "            if(convId == '')\r\n" +//được mời join vào chat conversation
                                "            {\r\n" +
                                "                convId = msg.convId;\r\n" +
                                "                convName = msg.convName;\r\n" +
                                "                LoadMsgs(0, msg.seq);\r\n" +
                                "            }\r\n" +
                                "            if(msg.convId == convId && (msg.type == " + (int)eChatMsgType.text + " || msg.type == " + (int)eChatMsgType.link + "))\r\n" +
                                "            {\r\n" +
                                "                DrawMessage(msg.message.content, msg.seq, msg.from, msg.createdTime);\r\n" +
                                "                if(msg.from != '" + user.LoginName + "'){\r\n" +
                                "                   if($('#txtChatContent').is(':focus')){\r\n" +
                                "                       ReadMsgReport(msg);\r\n" +
                                "                   }\r\n" +
                                "                   else{\r\n" +
                                "                       readMsg = msg;\r\n" +
                                "                   }\r\n" +
                                "                }\r\n" +
                                "            }\r\n" +
                                "        });\r\n" +
                                //Sự kiện được kích hoạt khi thay đổi trạng thái của msg (nhận đc báo cáo: "đã nhận", "đã đọc" tin nhắn của 1 người khác)
                                "       client.on('chatmessagestate', function (msg) {\r\n" +
                                "           if(msg.status == " + (int)eMsgReportStatus.read + ")\r\n" +
                                "           {\r\n" +
                                "               var divId = 'divReadUser' + msg.from.replace('.','');\r\n" +
                                "               $('#' + divId).remove();\r\n" +
                                "               var divReadUser = document.createElement('div');\r\n" +
                                "               divReadUser.setAttribute('id', divId);\r\n" +
                                "               AjaxOut = OneTSQ.WebParts.DT_DaoTaoTrucTuyen.DrawReadUser(RenderInfo, msg.from).value;\r\n" +
                                "               divReadUser.innerHTML=AjaxOut.HtmlContent;\r\n" +
                                "	            document.getElementById('divReadReport' + msg.lastMsgSeq).appendChild(divReadUser);\r\n" +
                                "           }\r\n" +
                                "       });\r\n" +
                #endregion
                #region Nhận về custommessage
                                "       client.on('custommessage', function (data) {\r\n" +
                                "           AjaxOut = OneTSQ.WebParts.DT_DaoTaoTrucTuyen.GetUserFullName(RenderInfo, data.fromUser).value;\r\n" +
                                "           var name=data.fromUser;\r\n" +
                                "           if(AjaxOut.Error != true)\r\n" +
                                "               name=AjaxOut.RetExtraParam1;\r\n" +
                #region Nhận được thông báo về giá trị giangVienServerId 
                                "           if(data.message.key==" + (int)eCustomMsgType.giangVienServerId + ") {\r\n" +
                               "                giangVienServerId = data.message.serverId;\r\n" +
                               "            }\r\n" +
                #endregion
                #region Nhận được thông báo về giá trị convId
                                "           if(data.message.key==" + (int)eCustomMsgType.convId + ") {\r\n" +
                               "                convId = data.message.convId;\r\n" +
                               "            }\r\n" +
                #endregion
                                "       });\r\n" +
                #endregion
                               "    }\r\n" +
                #endregion
                #region Các sự kiện room
                                "   function SettingRoomEvents(room1) {\r\n" +
                                "            room1.on('joinroom', function(event){\r\n" +
                                "                console.log('on join room: ' + JSON.stringify(event.info));\r\n" +
                                //Nếu convId chưa có thì kiểm tra lại giá trị convId. Nếu có rồi thì cập nhật. Nếu chưa có thì tạo conversation và cập nhật lại giá trị convId
                                "                if(convId == ''){\r\n" +
                                "                   convId = OneTSQ.WebParts.DT_DaoTaoTrucTuyen.GetConvId(RenderInfo, khoaHocId).value.RetExtraParam1;\r\n" +
                                "                   if(convId == '')\r\n" +
                                "                       CreateConv('" + khoaHoc.TEN + "', [event.info.user.userId]);\r\n" +
                                "                }\r\n" +
                                //Nếu user hiện tại là giảng viên thì Cập nhật on/off trên danh sách học viên
                                "                if(isGiangVien){\r\n" +
                                //Gửi thông điệp chứa serverId của video đại diện của giảng viên cho các user trong lớp
                                "                   var message = new Object();\r\n" +
                                "                   message.key=" + (int)eCustomMsgType.giangVienServerId + ";\r\n" +
                                "                   message.userId='" + user.LoginName + "';\r\n" +
                                "                   message.serverId=giangVienTrack.serverId;\r\n" +
                                "	                stringeeClient.sendCustomMessage(event.info.user.userId, message, function (res){\r\n" +
                                "                   });\r\n" +
                                "                   SetOnOff(event.info.user.userId, true);\r\n" +
                                "               }\r\n" +
                                "            });\r\n" +
                                "            room1.on('leaveroom', function(event){\r\n" +
                                "                console.log('on leave room: ' + JSON.stringify(event.info));\r\n" +
                                //Tìm trackInfo của user rời room
                                "               for(i=0; i<trackInfos.length; i++)\r\n" +
                                "               {\r\n" +
                                "                   if(trackInfos[i].userPublish==event.info.user.userId){\r\n" +
                                //Tìm subscribedTrack của user rời room
                                "                       for(j=0; j<subscribedTracks.length; j++)\r\n" +
                                "                       {\r\n" +
                                "                           if(subscribedTracks[j].serverId==trackInfos[i].serverId){\r\n" +
                                "	                            subscribedTracks.splice(j,1);\r\n" +
                                "                               break;\r\n" +
                                "                           }\r\n" +
                                "                       }\r\n" +
                                "	                    trackInfos.splice(i,1);\r\n" +
                                "                       break;\r\n" +
                                "                   }\r\n" +
                                "               }\r\n" +
                                //Remove user khỏi conversation
                                //Lẽ ra lệnh này chỉ thực hiện 1 lần đối với 1 user. Nhưng trong sự kiện này thì không biết nên gọi lệnh này ở phía user nào.
                                //Vì vậy gọi lệnh ở mọi user (chắc không vấn đề gì)
                                "               RemoveParticipant([event.info.user.userId]);\r\n" +
                                //Nếu user hiện tại là giảng viên thì Cập nhật on/off trên danh sách học viên
                                "               if(isGiangVien)\r\n" +
                                "                   SetOnOff(event.info.user.userId, false);\r\n" +
                                "            });\r\n" +
                                "            room1.on('addtrack', function(event){\r\n" +
                                "                console.log('on add track: ' + JSON.stringify(event.info));\r\n" +
                                "                var isLocalTrack = false;\r\n" +
                                "                var isExistedUser = false;\r\n" +
                                //Remove trackInfo trong trường hợp user trước đó đã unpublish video
                                "                for(i = trackInfos.length - 1; i >= 0; i-- ){\r\n" +
                                "                    if(trackInfos[i].userPublish == event.info.track.userPublish){\r\n" +
                                "                        isExistedUser = true;\r\n" +
                                "                        if(mainTrackInfo.serverId == trackInfos[i].serverId){\r\n" +//Thay đổi video ở divMainVideos
                                "                            mainTrackInfo = event.info.track;\r\n" +
                                "                        }\r\n" +
                                //Thay đổi video ở divVideos
                                "                        if(document.getElementById('divVideo' + trackInfos[i].serverId) != null){\r\n" +//Thay đổi video ở divMainVideos
                                "                           document.getElementById('divVideo' + trackInfos[i].serverId).id = 'divVideo' + event.info.track.serverId;\r\n" +
                                "                        }\r\n" +
                                "                        trackInfos.splice(i, 1);\r\n" +
                                "                        break;\r\n" +
                                "                    }\r\n" +
                                "                }\r\n" +

                                "                localTracks.forEach(function (localTrack) {\r\n" +
                                "                    if(localTrack.serverId === event.info.track.serverId) {\r\n" +
                                "                       console.log(localTrack.serverId + ' is LOCAL');\r\n" +
                                "                       isLocalTrack = true;\r\n" +
                                "                       if(isGiangVien == true){\r\n" +
                                "                           if(mainTrackInfo == undefined){\r\n" +
                                "                               mainTrackInfo = event.info.track;\r\n" +//Thiết lập track hiển thị ở khung chính
                                "                           }\r\n" +
                                "                       }\r\n" +
                                "                       if(isExistedUser){\r\n" +
                                //Hiển thị lại video ở divMainVideos
                                "                           if (localTrack.serverId==mainTrackInfo.serverId)\r\n" +
                                "                               ShowMainVideo(localTrack);\r\n" +
                                //Hiển thị lại video ở divVideos
                                "                           if(document.getElementById('divVideo' + event.info.track.serverId) != null){\r\n" +//Thay đổi video ở divMainVideos
                                "                               var videoElement = localTrack.attach();\r\n" +
                                "                               videoElement.setAttribute('style', 'width:100%; background: #424141;');\r\n" +
                                "                               document.getElementById('divVideos' + event.info.track.serverId).appendChild(videoElement);\r\n" +
                                "                           }\r\n" +
                                "                       }\r\n" +
                                "                       trackInfos.push(event.info.track);\r\n" +
                                "                    }\r\n" +
                                "                });\r\n" +
                                //Kiểm tra track video đại diện của giảng viên.
                                "                    if(isGiangVien == true && giangVienTrack != undefined && giangVienTrack.serverId === event.info.track.serverId) {\r\n" +
                                "                       console.log(giangVienTrack.serverId + ' is LOCAL and giang vien avartar');\r\n" +
                                "                       isLocalTrack = true;\r\n" +
                                "                       giangVienTrackInfo = event.info.track;\r\n" +//Thiết lập track hiển thị ở khung đại diện của giảng viên                                                                                                
                                "                    }\r\n" +

                                "                if(!isLocalTrack){\r\n" +
                                "                    Subscribe(event.info.track);\r\n" +
                                "                }\r\n" +

                                "            });\r\n" +
                                "            room1.on('removetrack', function(event)\r\n" +
                                "            {\r\n" +
                                "                console.log('on remove track: ' + event);\r\n" +
                                "                var track = event.track;\r\n" +
                                "                if (!track) {\r\n" +
                                "                    return;\r\n" +
                                "                }\r\n" +
                                "                var mediaElements = track.detach();\r\n" +
                                "                mediaElements.forEach(function (videoElement) {\r\n" +
                                "                    videoElement.remove();\r\n" +
                                "                });\r\n" +
                                "                for(i = subscribedTracks.length - 1; i >= 0; i-- ){\r\n" +
                                "                   if(subscribedTracks[i].serverId == track.serverId){\r\n" +
                                "                       subscribedTracks.splice(i, 1);\r\n" +
                                "                       break;\r\n" +
                                "                   }\r\n" +
                                "                }\r\n" +
                                "            });\r\n" +
                                "   }\r\n" +
                #endregion
                #endregion
                #region Refresh các biến và form về trạng thái ban đầu.
                               "    function RefreshNewState(){\r\n" +
                               "            room = undefined;\r\n" +
                               "            localTracks = [];\r\n" +
                               "            subscribedTracks = [];\r\n" +
                               "            trackInfos = [];\r\n" +
                               "            mainTrackInfo = undefined;\r\n" +
                               "            giangVienTrack = undefined;\r\n" +
                               "            giangVienTrackInfo = undefined;\r\n" +
                               "            giangVienServerId = undefined;\r\n" +
                               "            isPlay = true;\r\n" +
                               "            isUnmute = true;\r\n" +
                               "            isShareScreen = false;\r\n" +
                               "            readMsg = undefined;\r\n" +
                               "            $('#divMainVideos').html('');\r\n" +
                               "            $('#divVideos').html('');\r\n" +
                               "           $('#divGiangVienInfo').hide();\r\n" +
                               "           $('#divHocViens').hide();\r\n" +
                               "           $('#divChatDiscussion').html('');\r\n" +
                               "           $('#divChat').hide();\r\n" +
                               "           $('#spUserOnlineQuantity').html(0);\r\n" +
                               "           $('#btnLeaveRoom').hide();\r\n" +
                               "           $('#btnEnableDisableVideo').hide();\r\n" +
                               "           $('#btnMuteUnmute').hide();\r\n" +
                               "           $('#btnShareUnshare').hide();\r\n" +
                               "    }\r\n" +
                #endregion

                #region SetOnOff
                               "    function SetOnOff(userName, isOnline){\r\n" +
                               "        if(isOnline == true){\r\n" +
                               "	        $('#td' + userName.split('.')[1]).html('<i class=\"fa fa-toggle-on\" style=\"font-size:24px; color: green; \"></i>');\r\n" +
                               "        }\r\n" +
                               "        else{\r\n" +
                               "	        $('#td' + userName.split('.')[1]).html('<i class=\"fa fa-toggle-off\" style=\"font-size:24px; \"></i>');\r\n" +
                               "        }\r\n" +
                               "        $('#spUserOnlineQuantity').html($('.fa-toggle-on').length);\r\n" +
                               "    }\r\n" +
                #endregion
                #region GetAccessTokenAndConnectToStringee để tạo roomToken
                                "function GetAccessTokenAndConnectToStringee(client) {\r\n" +
                                "    $.getJSON(tokenUrl + '?userId=" + user.LoginName + "&roomId=' + roomId+'&rest=true', function(res) {\r\n" +
                                "        access_token = res.access_token;\r\n" +
                                "        rest_access_token = res.rest_access_token;\r\n" +
                                "        roomToken = res.room_token;\r\n" +
                                "        client.connect(access_token);\r\n" +
                                "        console.log('roomToken: ', roomToken);\r\n" +
                                "    });\r\n" +
                                "}\r\n" +
                #endregion
                #region Tạo và publish luồng video vào room
                                "   function CreatePublishTrack(screenSharing){\r\n" +//screenSharing==true: Publish Screen, screenSharing = false: Publish audio/video
                                "        var pubOptions = {\r\n" +
                                "           audio: true,\r\n" +
                                "           video: true,\r\n" +
                                "           screen: screenSharing\r\n" +
                                "       };\r\n" +
                                "    StringeeVideo.createLocalVideoTrack(stringeeClient, pubOptions).then(function(localTrack){\r\n" +
                                "        console.log('create Local Video Track success: ', localTrack);\r\n" +
                                "        localTracks.push(localTrack);\r\n" +
                                //        Hiển thị local video                               
                                "        if(isGiangVien == true)\r\n" +
                                "           ShowMainVideo(localTrack);\r\n" +
                                "       else\r\n" +
                                "           ShowVideo(localTrack);\r\n" +
                                //       publish
                                "        room.publish(localTrack).then(function ()\r\n" +
                                "        {\r\n" +
                                "            console.log('publish Local Video Track success: ' + localTrack.serverId);\r\n" +
                                "        }).catch(function(error1)\r\n" +
                                "        {\r\n" +
                                "            console.log('publish Local Video Track ERROR: ', error1);\r\n" +
                                "        });\r\n" +
                                "    }).catch(function(res)\r\n" +
                                "    {\r\n" +
                                "        console.log('create Local Video Track ERROR: ', res);\r\n" +
                                "    });\r\n" +
                                "   }\r\n" +
                #endregion
                #region Unpublish video từ room
                                "   function Unpublish() {\r\n" +
                                "    console.log('Unpublish');\r\n" +
                                "    localTracks.forEach(function(localTrack) {\r\n" +
                                "        room.unpublish(localTrack);\r\n" +
                                "        localTrack.detachAndRemove();\r\n" +
                                "    });\r\n" +
                                "    localTracks=[];\r\n" +
                                //remove track info
                                "                for(i = trackInfos.length - 1; i >= 0; i-- ){\r\n" +
                                "                    if(trackInfos[i].userPublish == '" + user.LoginName + "'){\r\n" +
                                "                        trackInfos.splice(i, 1);\r\n" +
                                "                    }\r\n" +
                                "                }\r\n" +
                                "   }\r\n" +
                #endregion
                #region Xử lý khi chạy trên trình duyệt Safari
                                "   function isSafari() {\r\n" +
                                "       var ua = navigator.userAgent.toLowerCase();\r\n" +
                                "       if (ua.indexOf('safari') != -1)\r\n" +
                                "       {\r\n" +
                                "           if (ua.indexOf('chrome') > -1)\r\n" +
                                "           {\r\n" +
                                "           }\r\n" +
                                "           else\r\n" +
                                "           {\r\n" +
                                "               return true;\r\n" +
                                "           }\r\n" +
                                "       }\r\n" +
                                "       return false;\r\n" +
                                "   }\r\n" +
                #endregion
                #region Subscribe 1 track
                                "   function Subscribe(trackInfo){\r\n" +
                                "       trackInfos.push(trackInfo);\r\n" +
                                "       var subOptions = {\r\n" +
                                "           audio: true,\r\n" +
                                "           video: true\r\n" +
                                "       };\r\n" +
                                "       room.subscribe(trackInfo.serverId, subOptions).then(function(track) {\r\n" +
                                "           console.log('subscribe success: ', track);\r\n" +
                                "           subscribedTracks.push(track);\r\n" +
                                "           track.on('ready', function() {\r\n" +
                                //Nếu track là của giảng viên
                                "               if (trackInfo.userPublish != '" + user.LoginName + "' && OneTSQ.WebParts.DT_DaoTaoTrucTuyen.CheckGiangVien(RenderInfo, khoaHocId, trackInfo.userPublish).value.RetObject==true){\r\n" +
                                "                   var interval_obj = setInterval(function(){\r\n" +
                                "                       if(giangVienServerId != undefined){\r\n" +
                                "                           clearInterval(interval_obj);\r\n" +
                                //Hiển thị video đại diện của giảng viên
                                "                           if(track.serverId == giangVienServerId){\r\n" +
                                "                               var videoElement = track.attach();\r\n" +
                                "                               videoElement.setAttribute('style', 'width:1px; background: #424141;');\r\n" +        
                                "                               document.getElementById('divGiangVienVideo').appendChild(videoElement);\r\n" +
                                "                           }\r\n" +
                                //Hiển thị video bài giảng của giảng viên
                                "                           else{\r\n" +
                                "                               mainTrackInfo = trackInfo;\r\n" +
                                "                               ShowMainVideo(track);\r\n" +
                                "                           }\r\n" +
                                "                       }\r\n" +
                                "                   }, 500);\r\n" +
                                "               }\r\n" +
                                //Nếu user hiện tại là giảng viên thì Cập nhật on/off trên danh sách học viên
                                "                else if(isGiangVien)\r\n" +
                                "                   SetOnOff(trackInfo.userPublish, true);\r\n" +
                                //Nếu chưa attach track của user publish track thì tạo div mới và attack track
                                "               if (document.getElementById('divVideo' + trackInfo.serverId)==null)\r\n" +
                                "                   ShowVideo(track);\r\n" +
                                //Nếu đã attach track của user publish track thì cập nhật lại track mới của div đang hiển thị track cũ.
                                "               else{\r\n" +
                                "                   var videoElement = track.attach();\r\n" +
                                "                   videoElement.setAttribute('style', 'width:100%; background: #424141;');\r\n" +                
                                "                   document.getElementById('divVideo' + trackInfo.serverId).appendChild(videoElement);\r\n" +
                                "               }\r\n" +
                                "           });\r\n" +
                                "       }).catch (function (res) {\r\n" +
                                "           console.log('subscribe ERROR: ', res);\r\n" +
                                "       });\r\n" +
                                "   }\r\n" +
                                "   function ShowMainVideo(track){\r\n" +
                                "       var videoElement = track.attach();\r\n" +
                                "       videoElement.setAttribute('style', 'width: 100%; background: #424141;');\r\n" +
                                "       if (isSafari())\r\n" +
                                "       {\r\n" +
                                "           videoElement.setAttribute('muted', 'true');\r\n" +
                                "       }\r\n" +
                                "       divMainVideos.innerHTML='';\r\n" +
                                "       divMainVideos.appendChild(videoElement);\r\n" +
                                "   }\r\n" +
                                "   function ShowVideo(track){\r\n" +
                                "       var div = document.createElement('div');\r\n" +
                                "       div.setAttribute('id', 'divVideo' + track.serverId);\r\n" +
                                "       div.setAttribute('style', 'float:left; width:1px;');\r\n" +
                                "       divVideos.appendChild(div);\r\n" +
                                "       var videoElement = track.attach();\r\n" +
                                "       videoElement.setAttribute('style', 'width:1px; background: #424141;');\r\n" +                              
                                "       document.getElementById('divVideo' + track.serverId).appendChild(videoElement);\r\n" +
                                "   }\r\n" +
                #endregion
                #region Join room, subscribe toàn bộ các luồng video trong room, tạo và publish localvideo
                                "   function JoinRoom(screenSharing){\r\n" +
                                "        StringeeVideo.joinRoom(stringeeClient, roomToken).then(function(data) {\r\n" +
                                "            console.log('join room success data: ', data);\r\n" +
                                "            listTracksInfo = data.listTracksInfo;\r\n" +
                                "            room = data.room;\r\n" +
                                //events
                                "            room.clearAllOnMethos();\r\n" +
                                "            SettingRoomEvents(room);\r\n" +
                                //subscribe
                                "            data.listTracksInfo.forEach(function(trackInfo)\r\n" +
                                "            {\r\n" +
                                "                Subscribe(trackInfo);\r\n" +
                                "            });\r\n" +
                                //create localVideo đại diện cho bác sỹ và publish nó
                                (isGiangVien ?
                                "           var pubOptions1 = {\r\n" +
                                "               audio: true,\r\n" +
                                "               video: true,\r\n" +
                                "               screen: false\r\n" +
                                "           };\r\n" +
                                "           StringeeVideo.createLocalVideoTrack(stringeeClient, pubOptions1).then(function(localTrack){\r\n" +
                                "               console.log('create Local Video Track success: ', localTrack);\r\n" +
                                "               giangVienTrack = localTrack;\r\n" +
                                //        Hiển thị video đại diện
                                "               var videoElement = localTrack.attach();\r\n" +
                                "               videoElement.setAttribute('style', 'width:1px; background: #424141;');\r\n" +
                                "               if (isSafari())\r\n" +
                                "               {\r\n" +
                                "               }\r\n" +
                                "               document.getElementById('divGiangVienVideo').appendChild(videoElement);\r\n" +
                                //          publish
                                "               room.publish(localTrack).then(function ()\r\n" +
                                "               {\r\n" +
                                "                   console.log('publish Local Video Track success: ' + localTrack.serverId);\r\n" +
                                "                   giangVienServerId = giangVienTrack.serverId;\r\n" +
                                //Gửi thông điệp chứa serverId của video đại diện của giảng viên cho các user trong lớp
                                "                   var message = new Object();\r\n" +
                                "                   message.key=" + (int)eCustomMsgType.giangVienServerId + ";\r\n" +
                                "                   message.userId='" + user.LoginName + "';\r\n" +
                                "                   message.serverId=giangVienTrack.serverId;\r\n" +
                                "                   console.log('giangVienTrack.serverId', giangVienTrack.serverId);\r\n" +
                                "		            for (var index=0;index< trackInfos.length;index++) {\r\n" +
                                "                       if(trackInfos[index].userPublish!='" + user.LoginName + "'){\r\n" +
                                "	                        stringeeClient.sendCustomMessage(trackInfos[index].userPublish, message, function (res){\r\n" +
                                "                           });\r\n" +
                                "                       }\r\n" +
                                "                   }\r\n" +
                                "               }).catch(function(error1)\r\n" +
                                "               {\r\n" +
                                "                   console.log('publish Local Video Track ERROR: ', error1);\r\n" +
                                "               });\r\n" +
                                "           }).catch(function(res)\r\n" +
                                "           {\r\n" +
                                "               console.log('create Local Video Track ERROR: ', res);\r\n" +
                                "           });\r\n" : null) +
                                "           var pubOptions = {\r\n" +
                                "               audio: true,\r\n" +
                                (isGiangVien ?
                                "               video: true,\r\n" +
                                "               screen: screenSharing\r\n" : null) +
                                "           };\r\n" +
                                "           StringeeVideo.createLocalVideoTrack(stringeeClient, pubOptions).then(function(localTrack){\r\n" +
                                "               console.log('create Local Video Track success: ', localTrack);\r\n" +
                                "               localTracks.push(localTrack);\r\n" +
                                //        Hiển thị local video
                                "               if (OneTSQ.WebParts.DT_DaoTaoTrucTuyen.CheckGiangVien(RenderInfo, khoaHocId, '" + user.LoginName + "').value.RetObject==true)\r\n" +
                                "                   ShowMainVideo(localTrack);\r\n" +
                                "               else\r\n" +
                                "                   ShowVideo(localTrack);\r\n" +
                                //          publish
                                "               room.publish(localTrack).then(function ()\r\n" +
                                "               {\r\n" +
                                "                   console.log('publish Local Video Track success: ' + localTrack.serverId);\r\n" +
                                "               }).catch(function(error1)\r\n" +
                                "               {\r\n" +
                                "                   console.log('publish Local Video Track ERROR: ', error1);\r\n" +
                                "               });\r\n" +
                                "           }).catch(function(res)\r\n" +
                                "           {\r\n" +
                                "               console.log('create Local Video Track ERROR: ', res);\r\n" +
                                "           });\r\n" +

                                "        }).catch(function(res)\r\n" +
                                "        {\r\n" +
                                "            console.log('join room ERROR: ', res);\r\n" +
                                "        });\r\n" +
                                "    }\r\n" +
                #endregion
                #region Unsubscribe luồng video
                                "   function UnJoinRoom() {\r\n" +
                                "    subscribedTracks.forEach(function(track) {\r\n" +
                                "        room.unsubscribe(track);\r\n" +
                                "        var mediaElements = track.detach();\r\n" +
                                "        mediaElements.forEach(function(videoElement) {\r\n" +
                                "            videoElement.remove();\r\n" +
                                "        });\r\n" +
                                "    });\r\n" +
                                "   }\r\n" +
                #endregion
                #region HangupCall: kết thúc cuộc gọi
                                "   function HangupCall() {\r\n" +
                                "    localTracks.forEach(function(localTrack) {\r\n" +
                                "        localTrack.close();\r\n" +
                                "    });\r\n" +
                                "   }\r\n" +
                #endregion
                #region Tìm track chính và hiển thị video của nó
                               "  function FindTrackAndShowMainVideo(){\r\n" +
                               // Set video chính cho form
                               "        for(i=0; i<localTracks.length; i++)\r\n" +
                               "        {\r\n" +
                               "            if(localTracks[i].serverId==mainTrackInfo.serverId){\r\n" +
                                "               ShowMainVideo(localTracks[i]);\r\n" +
                               "            }\r\n" +
                               "        }\r\n" +
                               "        for(i=0; i<subscribedTracks.length; i++)\r\n" +
                               "        {\r\n" +
                               "            if(subscribedTracks[i].serverId==mainTrackInfo.serverId){\r\n" +
                                "               ShowMainVideo(subscribedTracks[i]);\r\n" +
                               "            }\r\n" +
                               "        }\r\n" +
                               "  }\r\n" +
                #endregion

                #region Leave room
                               "    function LeaveRoom() {\r\n" +
                               "        Unpublish();\r\n" +
                               "        if(isGiangVien){\r\n" +
                               "            room.unpublish(giangVienTrack);\r\n" +
                               "            giangVienTrack.detachAndRemove();\r\n" +
                               "        }\r\n" +
                               "        room.leave(true);\r\n" +
                               "        RefreshNewState();\r\n" +
                               "    }\r\n" +
                #endregion
                #region MuteUnmute
                               "    function MuteUnmute() {\r\n" +
                               "	   if(!enableUnmuteByAdmin && !isUnmute)\r\n" +
                               "       {\r\n" +
                               "            callGallAlert(\"" + WebLanguage.GetLanguage(OSiteParam, "Bạn không được phép bật tiếng tại thời điểm này!") + "\") \r\n" +
                               "            return;\r\n" +
                               "       }\r\n" +
                               "        isUnmute = !isUnmute;\r\n" +
                               "        if(isUnmute)enableUnmute=true;\r\n" +
                               "        else enableUnmute=false;\r\n" +
                               "        localTracks.forEach(function(track) {\r\n" +
                               "            track.mute(!isUnmute);\r\n" +
                               "        });\r\n" +
                               "   }\r\n" +
                #endregion
                #region ShareUnshareScreen
                               "    function ShareUnshareScreen() {\r\n" +
                               "        isShareScreen = !isShareScreen;\r\n" +
                               "        Unpublish();\r\n" +
                               "        CreatePublishTrack(isShareScreen);\r\n" +
                               "   }\r\n" +
                #endregion
                #endregion

                #region chat
                #region Create conversation
                               "function CreateConv(convName, userArr) {\r\n" +
                               "    var options = {distinct: true, participants: userArr, isGroup: true, name: convName, localDbId: 'conv-local-1'};\r\n" +
                               "    stringeeChat.createConversation(options, function (res) {\r\n" +
                               "         console.log('createConversation: ', res)\r\n" +
                               "         if(res.r==0 || res.r==2){\r\n" +
                               "            convId = res.convId;\r\n" +
                                //Tạm thời phải add lại các user vào conversation thì mới chat được
                                //Gửi thông điệp chứa convId của conversion cho các user trong lớp và add các user vào conversation
                                "           var users = [];\r\n" +
                                "           for(i=0; i<trackInfos.length; i++)\r\n" +
                                "           {\r\n" +
                                "               if(trackInfos[i].userPublish != '" + user.LoginName + "'){\r\n" +
                                "                   isExists = false;\r\n" +
                                "                   for(j=0; j<users.length; j++)\r\n" +
                                "                   {\r\n" +
                                "                       if(trackInfos[i].userPublish == users[j])\r\n" +
                                "                       {\r\n" +
                                "                           isExists = true;\r\n" +
                                "                           break;\r\n" +
                                "                       }\r\n" +
                                "                   }\r\n" +
                                "                   if(isExists == false)\r\n" +
                                "                       users.push(trackInfos[i].userPublish);\r\n" +
                                "               }\r\n" +
                                "           }\r\n" +

                                "           for(i=0; i<res.participants.length; i++)\r\n" +
                                "           {\r\n" +
                                "               if(res.participants[i].user != '" + user.LoginName + "'){\r\n" +
                                "                   isExists = false;\r\n" +
                                "                   for(j=0; j<users.length; j++)\r\n" +
                                "                   {\r\n" +
                                "                       if(res.participants[i].user == users[j])\r\n" +
                                "                       {\r\n" +
                                "                           isExists = true;\r\n" +
                                "                           break;\r\n" +
                                "                       }\r\n" +
                                "                   }\r\n" +
                                "                   if(isExists == false)\r\n" +
                                "                       users.push(res.participants[i].user);\r\n" +
                                "               }\r\n" +
                                "           }\r\n" +
                               "            AddParticipant(users);\r\n" +

                               "            for(j=0; j<users.length; j++)\r\n" +
                               "            {\r\n" +
                               "               var message = new Object();\r\n" +
                               "               message.key=" + (int)eCustomMsgType.convId + ";\r\n" +
                               "               message.userId='" + user.LoginName + "';\r\n" +
                               "               message.convId=convId;\r\n" +
                               "	            stringeeClient.sendCustomMessage(users[j], message, function (res){\r\n" +
                               "               });\r\n" +
                               "            }\r\n" +

                               "            OneTSQ.WebParts.DT_DaoTaoTrucTuyen.UpdateConvId(RenderInfo, khoaHocId, convId).value;\r\n" +
                               "         }\r\n" +
                               "    });\r\n" +
                               " }\r\n" +
                #endregion
                #region AddParticipant
                                "function AddParticipant(userArr) {\r\n" +
                                "   var body = {convId: convId, userIds: userArr};\r\n" +
                                "   stringeeChat.addParticipant(body,function (res) {\r\n" +
                                "       LoadMsgs(0, res.requestId);\r\n" +
                                "       console.log('addParticipant: ', res)\r\n" +
                                "   });\r\n" +
                                " }\r\n" +
                #endregion
                #region RemoveParticipant
                                "function RemoveParticipant(userArr) {\r\n" +
                                "    var body = {convId: convId, userIds: userArr};\r\n" +
                                "    stringeeChat.removeParticipant(body,function (res) {\r\n" +
                                "       console.log('removeParticipant: ', res)\r\n" +
                                "    });\r\n" +
                                "}\r\n" +
                #endregion
                #region Send Message
                                "function SendMessage(msg, type) {\r\n" +
                                "    var message = {content: msg};\r\n" +
                                "    var body = {message: message, type: type, convId: convId};\r\n" +
                                "    stringeeChat.sendChatMessage(body, function (res) {\r\n" +
                                "       console.log('sendChatMessage', res);\r\n" +
                                "       if(res.r == 0){\r\n" +
                                //Lấy về object msg vừa gửi để hiển thị
                                "               DrawMessage(msg, res.seq, '" + user.LoginName + "', res.created);\r\n" +
                                "       }\r\n" +
                                "    });\r\n" +
                                "}\r\n" +
                #endregion
                #region DrawMessage
                "  function DrawMessage(msg, msgSeq, fromUser, createDate){\r\n" +
                               "     var divMsg = document.createElement('div');\r\n" +
                               "     AjaxOut = OneTSQ.WebParts.DT_DaoTaoTrucTuyen.DrawMessage(RenderInfo, msg, msgSeq, fromUser, moment(createDate).format('LT')).value;\r\n" +
                               "     divMsg.innerHTML=AjaxOut.HtmlContent;\r\n" +
                               "	 document.getElementById('divChatDiscussion').appendChild(divMsg);\r\n" +
                               "  }\r\n" +
                #endregion
                #region Thông báo đã đọc tin nhắn
                               "function ReadMsgReport(msg) {\r\n" +
                               "    stringeeChat.markMessageSeen(msg, function (res) {\r\n" +
                               "       console.log('markMessageSeen', res);\r\n" +
                               "    });\r\n" +
                               " }\r\n" +
                #endregion
                #region Gọi hàm gửi thông báo đã đọc tin nhắn
                               "function CallReadMsgReport(sender) {\r\n" +
                               "   if(readMsg != undefined && (sender.getAttribute('id') == 'txtChatContent' || sender.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.nextElementSibling.getAttribute('id')=='divReadReport' + readMsg.seq)){\r\n" +
                               "      ReadMsgReport(readMsg);\r\n" +
                               "      readMsg = undefined;\r\n" +
                               "    }\r\n" +
                               "}\r\n" +
                #endregion
                #region LoadMsgs
                                "function LoadMsgs(seqGreater, msgQuantity) {\r\n" +
                                "    var body = {limit: msgQuantity, seqGreater: seqGreater, sort: 'ASC', convId: convId};\r\n" +
                                "    stringeeChat.loadChatMessages(body, function (res) { \r\n" +
                                "       if(res.msgs != undefined){\r\n" +//hiển thị các tin nhắn trước đó
                                "           msgsQuantity = res.msgs.length;\r\n" +
                                "           for(var i = 0 ; i < msgsQuantity ; i++)\r\n" +
                                "           {\r\n" +
                                "               if(res.msgs[i].type == " + (int)eChatMsgType.text + " || res.msgs[i].type == " + (int)eChatMsgType.link + ") \r\n" +
                                "                   DrawMessage(res.msgs[i].content.content, res.msgs[i].seq, res.msgs[i].user, res.msgs[i].created);\r\n" +
                                "           }\r\n" +
                                "       }\r\n" +
                                "    }); \r\n" +
                                "}\r\n" +
                #endregion

                #region Kiểm tra định dạng file
                    "   function CheckFileTaiLieuType(files) \r\n" +
                    "   {\r\n" +
                    "       if (files.length > 0) \r\n" +
                    "       {\r\n" +
                    "           for(var i = 0; i < files.length; i++){\r\n" +
                    "               fileExtension = files[i].name.split('.').pop().toLowerCase();\r\n" +
                    "               if(fileExtension == 'bmp' || fileExtension == 'jpg' || fileExtension == 'jpeg' || fileExtension == 'jpe' || fileExtension == 'jfif' || fileExtension == 'gif' || fileExtension == 'tif' || fileExtension == 'tiff' || fileExtension == 'png' || fileExtension == 'heic'\r\n" +
                    "                   || fileExtension == 'zip' || fileExtension == 'rar' || fileExtension == 'doc' || fileExtension == 'docx' || fileExtension == 'xls' || fileExtension == 'xlsx' || fileExtension == 'ppt' || fileExtension == 'pptx' || fileExtension == 'pdf' " +
                    "                   || fileExtension == 'mp3' || fileExtension == 'mp4' || fileExtension == 'mov' || fileExtension == 'mpeg4' || fileExtension == 'avi' || fileExtension == 'wmv' || fileExtension == 'mpegps' || fileExtension == 'flv' || fileExtension == '3gpp')\r\n" +
                    "               {\r\n" +
                    "                   return true;\r\n" +
                    "               }\r\n" +
                    "           };\r\n" +
                    "           return false;\r\n" +
                    "       }\r\n" +
                    "       return true;\r\n" +
                    "   }\r\n" +
                #endregion
                #region Send file
                                "function SendFiles(files)\r\n" +
                                "{\r\n" +
                                "   if (!CheckFileTaiLieuType(files)) \r\n" +
                                "   {\r\n" +
                                "       callGallAlert('Định dạng file không được hỗ trợ. Xin chọn file có định dạng khác.');\r\n" +
                                "       return;\r\n" +
                                "   }\r\n" +
                                "    var data = new FormData();\r\n" +
                                "    for (i = 0; i < files.length; i++) {\r\n" +
                                "        data.append(''+i, files[i]);\r\n" +
                                "    }\r\n" +
                                "   var ajaxRequest = $.ajax({\r\n" +
                                "       type: 'POST',\r\n" +
                                "       url: '" + WebConfig.GetUploadHandler(OSiteParam, SessionId, user.OwnerUserId, user.LoginName/*, "UploadedChatFilePath"*/) + "',\r\n" +
                                "       contentType: false,\r\n" +
                                "       processData: false,\r\n" +
                                "       data: data,\r\n" +
                                "       success: function(xmlResult) {\r\n" +
                                "           xmlDataText = $(xmlResult).find('XmlData').text();\r\n" +
                                "           xmlDataDom = new window.DOMParser().parseFromString(xmlDataText, 'text/xml');\r\n" +
                                "           files = xmlDataDom.getElementsByTagName('data');\r\n" +
                                "           var msg='';\r\n" +
                                "           for (j = 0; j < files.length; j++) {\r\n" +
                                "               uploadFileName = files[j].getElementsByTagName('UploadFileName')[0].textContent;\r\n" +
                                "               uploadUrl = files[j].getElementsByTagName('UploadUrl')[0].textContent;\r\n" +
                                "               fileType = uploadFileName.substring(uploadFileName.lastIndexOf('.') + 1);\r\n" +
                                "               if (['doc', 'docx', 'xls', 'xlsx', 'ppt', 'pptx' ].indexOf(fileType) != -1)\r\n" + //Nếu là các file thuộc office thì mở nhờ vào http:'docs.google.com/viewer
                                "               {\r\n" +
                                //"                   Tạm thời chưa có tên miền nên người dùng tự download về\r\n" +
                                "                   msg += \"<a href='\" + uploadUrl + \"' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở") + "' target='_blank' onclick='CallReadMsgReport(this)'>\" + uploadFileName + \"</a><br>\";\r\n" +
                                "               }\r\n" +
                                "               else\r\n" +
                                "               {\r\n" +
                                "                   msg += \"<a href='\" + uploadUrl + \"' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở") + "' target='_blank' onclick='CallReadMsgReport(this)'>\" + uploadFileName + \"</a><br>\";\r\n" +
                                "               }\r\n" +
                                "           }\r\n" +
                                "           msg = msg.substr(0,msg.length-4)\r\n" +
                                "           SendMessage(msg, " + (int)eChatMsgType.link + ");\r\n" +
                                "           $('#fulFile')[0].value='';\r\n" +
                                "       },\r\n" +
                                "       error: function(result) {\r\n" +
                                "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Lỗi upload file.") + "');\r\n" +
                                "       },\r\n" +
                                "   });\r\n" +
                                "}\r\n" +
                #endregion
                #endregion
                #region Hiển thị row thêm mới thành viên room
                                "   function ShowAddRoomMember(){\r\n" +
                                "       $('.CssEditorItemThanhVien').hide();\r\n" +
                                "       $('.CssDisplayItemThanhVien').show();\r\n" +
                                "       $('#trAddThanhVien').show();\r\n" +

                                "       RenderInfo=CreateRenderInfo();\r\n" +
                                "       AjaxOut = OneTSQ.WebParts.DT_DaoTaoTrucTuyen.CbbBacSy(RenderInfo, null, null).value;\r\n" +
                                "       $('#divCbbBacSy').html(AjaxOut);\r\n" +
                                "       CallInitSelect2('cbbBacSy', '" + WebEnvironments.GetRemoteProcessDataUrl(BacSyService.StaticServiceId) + "');\r\n" +
                                "       $('#cbbBacSy').removeClass('select2-hidden-accessible');\r\n" +
                                "       $('#cbbBacSy').css('display','none');\r\n" +
                                "   }\r\n" +
                #endregion
                #region Bắt sự kiện thay đổi combobox lịch hội chẩn
                                "   function cbbLichHoiChan_onchange(sender)\r\n" +
                                "   {\r\n" +
                                "       RenderInfo=CreateRenderInfo();\r\n" +
                                "       AjaxOut = OneTSQ.WebParts.DT_DaoTaoTrucTuyen.GetThanhVienHoiChans(RenderInfo, sender.value).value;\r\n" +
                                "       $('#divRoomMemberList').html(AjaxOut.HtmlContent);\r\n" +
                                "   }\r\n" +
                #endregion
                #region Bắt sự kiện thay đổi combobx thành viên trên lưới
                                "   function cbbBacSy_OnChange(sender, rowIndex)\r\n" +
                                "   {\r\n" +
                                "       RenderInfo=CreateRenderInfo();\r\n" +
                                "       AjaxOut = OneTSQ.WebParts.DT_DaoTaoTrucTuyen.GetThongTinBacSy(RenderInfo, sender.value).value;\r\n" +
                                "       if(AjaxOut.JsonData != ''){\r\n" +
                                "           bacSy = JSON.parse(AjaxOut.JsonData);\r\n" +
                                "           $('#hdBacSyFullName'+(rowIndex==undefined?'':rowIndex)).val(bacSy.bacSyFullName);\r\n" +
                                "           $('#spMa'+(rowIndex==undefined?'':rowIndex)).html(bacSy.bacSyCode);\r\n" +
                                "           $('#spDonVi'+(rowIndex==undefined?'':rowIndex)).html(bacSy.donViCongTac);\r\n" +
                                "           $('#spLoginName'+(rowIndex==undefined?'':rowIndex)).html(bacSy.loginName);\r\n" +
                                "       }\r\n" +
                                "       else{\r\n" +
                                "           $('#hdBacSyFullName'+(rowIndex==undefined?'':rowIndex)).val('');\r\n" +
                                "           $('#spMa'+(rowIndex==undefined?'':rowIndex)).html('');\r\n" +
                                "           $('#spDonVi'+(rowIndex==undefined?'':rowIndex)).html('');\r\n" +
                                "           $('#spLoginName'+(rowIndex==undefined?'':rowIndex)).html('');\r\n" +
                                "       }\r\n" +
                                "   }\r\n" +
                #endregion
                #region Thêm mới/cập nhật thành viên room
                                "   function SaveRoomMember(rowIndex){\r\n" +
                                "       chuyenGiaHoiChanId = document.getElementById('hdChuyenGiaHoiChanId'+rowIndex).value;\r\n" +
                                "       bacSyFullName = document.getElementById('hdBacSyFullName'+rowIndex).value;\r\n" +
                                "       bacSyId = document.getElementById('cbbBacSy'+rowIndex).value;\r\n" +
                                "       ma = $('#spMa'+rowIndex).html();\r\n" +
                                "       donVi = $('#spDonVi'+rowIndex).html();\r\n" +
                                "       loginName = $('#spLoginName'+rowIndex).html();\r\n" +
                                "       if(bacSy=='')\r\n" +
                                "       {\r\n" +
                                "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn thành viên tham gia hội chẩn.") + "');\r\n" +
                                "           return;\r\n" +
                                "       }\r\n" +
                                "       RenderInfo=CreateRenderInfo();\r\n" +
                                "       AjaxOut = OneTSQ.WebParts.DT_DaoTaoTrucTuyen.SaveRoomMember(RenderInfo, chuyenGiaHoiChanId, bacSyId, ma, bacSyFullName, donVi, loginName).value;\r\n" +
                                "       if(AjaxOut.Error)\r\n" +
                                "       {\r\n" +
                                "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                                "           return;\r\n" +
                                "       }\r\n" +
                                "       $('#divRoomMemberList').html(AjaxOut.HtmlContent);\r\n" +
                                "   }\r\n" +
                #endregion
                #region Xóa thành viên hội chẩn
                                "   function DeleteRoomMember(rowIndex){\r\n" +
                                "       chuyenGiaHoiChanId = document.getElementById('hdChuyenGiaHoiChanId'+rowIndex).value;\r\n" +
                                "       RenderInfo=CreateRenderInfo();\r\n" +
                                "       AjaxOut = OneTSQ.WebParts.DT_DaoTaoTrucTuyen.DeleteRoomMember(RenderInfo, chuyenGiaHoiChanId).value;\r\n" +
                                "       if(AjaxOut.Error)\r\n" +
                                "       {\r\n" +
                                "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                                "           return;\r\n" +
                                "       }\r\n" +
                                "       $('#divRoomMemberList').html(AjaxOut.HtmlContent);\r\n" +
                                "   }\r\n" +
                #endregion
                "</script>\r\n") +
                #endregion
                #region Html
                WebEnvironments.ProcessHtml(
                   "<div  id=\"divContent\" class=\"ibox float-e-margins\" style='position: relative; z-index:10;overflow: hidden;'> \r\n" +
                   "   <div class=\"col-md-9\">\r\n" +
                   "       <div class=\"row\">\r\n" +
                   "         <button class=\"btn btn-sm btn-primary\" id='btnLeaveRoom' onclick=\"LeaveRoom()\">Rời lớp</button>\r\n" +
                   "         <button class=\"btn btn-sm btn-primary\" id='btnMuteUnmute' onclick=\"MuteUnmute()\">Bât/tắt tiếng</button>\r\n" +
                   "         <button class=\"btn btn-sm btn-primary\" id='btnShareUnshare' onclick=\"ShareUnshareScreen()\" style='" + (isGiangVien ? null : "display:none;") + "'>Share/Unshare Screen</button>\r\n" +
                   "         <label>Logged in:</label> <span id=\"loggedUserId\" style=\"color: red\">Not logged</span>\r\n" +
                   "       </div>\r\n" +
                   "       <div class=\"row\">\r\n" +
                   "              <div id='divMainVideos' style='border: 1px solid black; border-color: #4682B4'>\r\n" +
                   "              </div>\r\n" +
                   "              <div id='divVideos'>\r\n" +
                   "              </div>\r\n" +
                   "       </div>\r\n" +
                   "       <div class=\"row\" style='font-weight: bold;'>\r\n" +
                   "            <div style='float:left; font-size: 24px;'>\r\n" +
                                    lichLyThuyetChiTiet.NOIDUNG +
                   "            </div>\r\n" +
                   "            <div style='float:right;'>\r\n" +
                                    (lichLyThuyetChiTiet.THOIGIAN == null ? lichLyThuyetChiTiet.NGAY.ToString("dd/MM/yyyy") : lichLyThuyetChiTiet.THOIGIAN.Value.ToString("HH:mm dd/MM/yyyy")) +
                   "            </div>\r\n" +
                   "       </div>\r\n" +
                   "   </div>\r\n" +
                   "   <div class=\"col-md-3\">\r\n" +
                   "       <div class=\"ibox float-e-margins\" id='divGiangVienInfo' >\r\n" +
                   "            <div class=\"ibox-content\" >\r\n" +
                   "                <center>\r\n" +              
                   "                    <div id='divGiangVienVideo'>\r\n" +
                   "                    </div>\r\n" +
                   "                </center>\r\n" +
                   "                <b>" + giangVien.HOTEN + "</b><br>\r\n" +
                   "                <b>" + (chucDanh == null ? null : chucDanh.Ten) + ": " + (chuyenKhoa == null ? null : chuyenKhoa.Ten) + "</b>\r\n" +
                   "            </div>\r\n" +
                   "       </div>\r\n" +
                    "        <div class='ibox float-e-margins' id=\"divHocViens\" " + (isHocVien ? "style='display:none;'" : null) + ">\r\n" +
                    "           <div class=\"ibox-title\">\r\n" +
                    "               <h5>" + WebLanguage.GetLanguage(OSiteParam, "Danh sách học viên: ") + " <span id='spUserOnlineQuantity'>0</span>/" + hocViens.Count() + "</h5>\r\n" +
                    "               <div class='ibox-tools'>\r\n" +
                    "                   <a class='collapse-link'>\r\n" +
                    "                   <i class='fa fa-chevron-up'></i>\r\n" +
                    "                   </a>\r\n" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "           <div class=\"ibox-content\" style='display: block;'>\r\n" +
                                    DrawHocViens(ORenderInfo, khoaHocId).HtmlContent +
                    "           </div>\r\n" +
                    "       </div>\r\n" +

                    "        <div class='ibox float-e-margins' id='divChat'>\r\n" +
                    "           <div class=\"ibox-title\">\r\n" +
                    "               <h5>" + WebLanguage.GetLanguage(OSiteParam, "Thảo luận") + "</h5>\r\n" +
                    "               <div class='ibox-tools'>\r\n" +
                    "                   <a class='collapse-link'>\r\n" +
                    "                   <i class='fa fa-chevron-up'></i>\r\n" +
                    "                   </a>\r\n" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "           <div class=\"ibox-content\" style='display: block;'>\r\n" +
                    "               <div class='row'>\r\n" +
                    "                   <div class=\"col-md-12\" style=''>\r\n" +
                    "                       <div class=\"chat-discussion\" id=\"divChatDiscussion\">\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "                   <div class=\"col-md-12\" style=''>\r\n" +
                    "                       <textarea id=\"txtChatContent\" class=\"form-control\" style='background-color: white; padding-right:40px;width: 80%; float: left;' name=\"message\" rows=2 placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tin nhắn") + "\" onfocus='CallReadMsgReport(this)'></textarea>\r\n" +
                    "                       <div id='divOtherMsgType' >\r\n" +
                    "                           <button class='btn btn-default btn-file fa fa-files-o' onkeypress=\"if(event.keyCode==13){document.getElementById('fulFile').click(); return false;}\" style='font-size:24px; border:0; background-color:transparent;'> \r\n " +
                    "                           <input id='fulFile' type='file' multiple onchange='SendFiles(this.files)' />\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                   "        </div>\r\n" +
                   "   </div>\r\n" +
               "</div>\r\n"
                );
                #endregion

            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.HtmlContent = ex.Message.ToString();
                RetAjaxOut.InfoMessage = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        #region Vẽ giao diện
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DrawHocViens(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                DT_HocVienCls[] hocViens = CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().Reading(ORenderInfo, new DT_HocVienFilterCls() { KhoaHocDuyet_Id = khoaHocId });
                int hocVienQuantity = hocViens.Count();
                string html = "";
                html =
                        "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                            "<thead> \r\n" +
                                "<tr> \r\n" +
                                "<th width=30 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                                "<th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Mã HV") + " </th> \r\n" +
                                "<th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Tên HV") + " </th> \r\n" +
                                "<th width=70 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "TT") + " </th> \r\n" +
                                "</tr> \r\n" +
                            "</thead> \r\n" +
                            "<tbody> \r\n";
                for (int iIndex = 0; iIndex < hocVienQuantity; iIndex++)
                {
                    html += "<tr> \r\n" +
                                "<td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                                "<td>" + hocViens[iIndex].MA + "</td> \r\n" +
                                "<td>" + hocViens[iIndex].HOTEN + "</td> \r\n" +
                                "<td style='text-align: center;' id='td" + hocViens[iIndex].MA + "'><i class=\"fa fa-toggle-off\" style=\"font-size:24px; \"></i></td> \r\n" +
                            "</tr> \r\n";
                }
                html += "</tbody> \r\n" +
                    "</table> \r\n";
                RetAjaxOut.HtmlContent = WebEnvironments.ProcessHtml(html);
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut SetOnOff(RenderInfoCls ORenderInfo, string userId, string serverId, string useradmin, bool isPlay, bool isUnmute)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                var ou = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, userId);
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                OwnerUserCls currentUser = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                string Html = null;
                if (ou != null)
                {
                    string avatar = null;
                    string hoTen = null;
                    BacSyOwnerUserCls[] bacSyOwnerUsers = CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Reading(ORenderInfo, new BacSyOwnerUserFilterCls() { OWNERUSERID = ou.OwnerUserId });
                    foreach (var bacSyOwnerUser in bacSyOwnerUsers)
                    {
                        BacSyCls bacSy = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, bacSyOwnerUser.BACSYID);
                        if (bacSy != null)
                        {
                            avatar = bacSy.NDANH;
                            hoTen = bacSy.HOTEN;
                            break;
                        }
                    }
                    Html =
                        "<div class=\"contact-box center-version\">\r\n" +
                    "    <a href=\"profile.html\">\r\n" +
                            (string.IsNullOrEmpty(avatar) ? "<i class='fa fa-user img-circle' style='font-size:100px;'> </i>\r\n" : "<img alt=\"image\" class=\"img-circle\" src='" + avatar + "' />\r\n") +
                    "       <h3 class=\"m-b-xs\"><strong>" + hoTen + "</strong></h3>\r\n" +
                    "       <div class=\"font-bold\">" + (userId == useradmin ? "Room Admin" : "Room member") + "</div>\r\n" +
                    "    </a>\r\n" +
                    "    <div class=\"contact-box-footer\">\r\n" +
                    "        <div class=\"m-t-xs btn-group\">\r\n" +
                    "            <a title='" + WebLanguage.GetLanguage(OSiteParam, "Bật/tắt hình") + "' href='javascript:CallEnableDisableVideo(\"" + serverId + "\", \"" + userId + "\");'><i id='iVideo" + serverId + "' class='iVideo fa " + (isPlay ? "fa-stop" : "fa-play") + "' style='font-size:20px;'></i></a>&nbsp;&nbsp;&nbsp;\r\n" +

                    //"            <a title='" + WebLanguage.GetLanguage(OSiteParam, "Bật hình") + "' href='javascript:SetVideoFloor(\"" + serverId + "\");'><i id='iVideo" + serverId + "' class='iVideo fa " + (isPlay ? "fa-stop" : "fa-play") + "' style='font-size:20px;'></i></a>&nbsp;&nbsp;&nbsp;\r\n" +
                    "            <a title='" + WebLanguage.GetLanguage(OSiteParam, "Bật/tắt tiếng") + "' href='javascript:CallMuteUnmute(\"" + serverId + "\", \"" + userId + "\");'><i id='iAudio" + serverId + "' class='fa " + (isUnmute ? "fa-volume-off" : "fa-volume-up") + "' style='font-size:20px;'></i></a>&nbsp;&nbsp;&nbsp;\r\n" +
                    (currentUser.LoginName == useradmin && userId != useradmin ? "<a title='" + WebLanguage.GetLanguage(OSiteParam, "Mời rời khỏi phòng") + "' href='javascript:CallRemoveUser(\"" + userId + "\");'><i class='fa fa-cut' style='font-size:20px;'></i></a>\r\n" : null) +
                    "        </div>\r\n" +
                    "    </div>\r\n" +
                    "</div>\r\n";
                }
                else
                {
                    Html =
                         "<div class=\"contact-box center-version\">\r\n" +
                    "    <a href=\"profile.html\">\r\n" +
                            "<i class='fa fa-user img-circle' style='font-size:100px;'> </i>\r\n" +
                    "       <h3 class=\"m-b-xs\" style='height: 26px;'><strong>Guest(\"" + userId + "\")</strong></h3>\r\n" +
                    "       <div class=\"font-bold\">" + (userId == useradmin ? "Room Admin" : "Room member") + "</div>\r\n" +
                    "    </a>\r\n" +
                     "    <div class=\"contact-box-footer\">\r\n" +
                    "        <div class=\"m-t-xs btn-group\">\r\n" +
                    "            <a title='" + WebLanguage.GetLanguage(OSiteParam, "Bật/tắt hình") + "' href='javascript:CallEnableDisableVideo(\"" + serverId + "\", \"" + userId + "\");'><i id='iVideo" + serverId + "' class='iVideo fa " + (isPlay ? "fa-stop" : "fa-play") + "' style='font-size:20px;'></i></a>&nbsp;&nbsp;&nbsp;\r\n" +
                    "            <a title='" + WebLanguage.GetLanguage(OSiteParam, "Bật/tắt tiếng") + "' href='javascript:CallMuteUnmute(\"" + serverId + "\", \"" + userId + "\");'><i id='iAudio" + serverId + "' class='fa " + (isUnmute ? "fa-volume-off" : "fa-volume-up") + "' style='font-size:20px;'></i></a>&nbsp;&nbsp;&nbsp;\r\n" +
                    (currentUser.LoginName == useradmin && userId != useradmin ? "<a title='" + WebLanguage.GetLanguage(OSiteParam, "Mời rời khỏi phòng") + "' href='javascript:CallRemoveUser(\"" + userId + "\");'><i class='fa fa-cut' style='font-size:20px;'></i></a>\r\n" : null) +
                    "        </div>\r\n" +
                    "    </div>\r\n" +
                    "</div>\r\n";
                }


                RetAjaxOut.HtmlContent = WebEnvironments.ProcessHtml(Html);
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut GetUserFullName(RenderInfoCls ORenderInfo, string loginName)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                string fullName = null;
                var ou = Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, loginName);
                if (ou != null)
                {
                    fullName = ou.FullName;
                    BacSyOwnerUserCls[] bacSyOwnerUsers = CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Reading(ORenderInfo, new BacSyOwnerUserFilterCls() { OWNERUSERID = ou.OwnerUserId });
                    foreach (var bacSyOwnerUser in bacSyOwnerUsers)
                    {
                        BacSyCls bacSy = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, bacSyOwnerUser.BACSYID);
                        if (bacSy != null)
                        {
                            fullName = bacSy.HOTEN;
                            break;
                        }
                    }
                }
                RetAjaxOut.RetExtraParam1 = fullName;
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.HtmlContent = ex.Message.ToString();
                RetAjaxOut.InfoMessage = ex.Message.ToString();
            }
            return RetAjaxOut;
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DrawMessage(RenderInfoCls ORenderInfo, string msg, int msgSeq, string fromUser, string time)//GMT
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                string userLocalLoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                var ou = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, fromUser);
                bool isMyMsg = userLocalLoginName == fromUser;
                string html = "";
                if (ou != null)
                {
                    string avatar = null;
                    string hoTen = ou.FullName;
                    BacSyOwnerUserCls[] bacSyOwnerUsers = CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Reading(ORenderInfo, new BacSyOwnerUserFilterCls() { OWNERUSERID = ou.OwnerUserId });
                    foreach (var bacSyOwnerUser in bacSyOwnerUsers)
                    {
                        BacSyCls bacSy = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, bacSyOwnerUser.BACSYID);
                        if (bacSy != null)
                        {
                            avatar = bacSy.NDANH;
                            hoTen = bacSy.HOTEN;
                            break;
                        }
                    }
                    if (isMyMsg)
                        html =
                        "<div class=\"row\" style='margin-bottom:5px; border-radius:10px;'>\r\n" +
                        "   <table style='float:right; max-width:90%;'>\r\n" +
                        "       <row>\r\n" +
                        "           <td style='background-color:#DBF4FD;'>\r\n" +
                        "               <div class=\"message\">\r\n" +
                        "                   <div class=\"row\" style=\"text-align:right;\">\r\n" +
                        "                        <a class=\"message-author\" href=\"#\">" + hoTen + "</a>\r\n" +
                        "			            <span class=\"message-date\" style=\"word-wrap: break-word;\"> " + time + " </span>\r\n" +
                        "                  </div>\r\n" +
                        "                  <span class=\"message-content\">\r\n" +
                                              msg +
                        "                  </span>\r\n" +
                        "                </div>\r\n" +
                        "           </td>\r\n" +
                        "           <td style='vertical-align:top;'>\r\n" +
                                    (string.IsNullOrEmpty(avatar) ? "<i class='fa fa-user img-circle' style='font-size:50px;'> </i>\r\n" : "<img alt=\"image\" style=\"width:40px;height:40px\" class=\"img-circle " + (isMyMsg == true ? "pull-right" : "") + "\" src='" + avatar + "' />\r\n") +
                        "           </td>\r\n" +
                        "       </row>\r\n" +
                        "   </table>\r\n" +
                        "   <div id='divReadReport" + msgSeq + "' style='width:100%; float:right; text-align:right; padding-right:50px;'>\r\n" +
                        "   </div>\r\n" +
                        "</div>\r\n";
                    else
                        html =
                        "<div class=\"row\" style='margin-bottom:5px;'>\r\n" +
                        "   <table style='max-width:90%;'>\r\n" +
                        "       <row>\r\n" +
                        "           <td style='vertical-align:top;'>\r\n" +
                                    (string.IsNullOrEmpty(avatar) ? "<i class='fa fa-user img-circle' style='font-size:50px;'> </i>\r\n" : "<img alt=\"image\" style=\"width:40px;height:40px\" class=\"img-circle " + (isMyMsg == true ? "pull-right" : "") + "\" src='" + avatar + "' />\r\n") +
                        "           </td>\r\n" +
                        "           <td style='background-color:#F2F6F9; border-radius:10px;'>\r\n" +
                        "               <div class=\"message\">\r\n" +
                        "                   <div class=\"row\">\r\n" +
                        "                        <a class=\"message-author\" href=\"#\">" + hoTen + "</a>\r\n" +
                        "			            <span class=\"message-date\" style=\"word-wrap: break-word;\"> " + time + " </span>\r\n" +
                        "                  </div>\r\n" +
                        "                  <span class=\"message-content\">\r\n" +
                                              msg +
                        "                  </span>\r\n" +
                        "                </div>\r\n" +
                        "           </td>\r\n" +
                        "       </row>\r\n" +
                        "   </table>\r\n" +
                        "   <div id='divReadReport" + msgSeq + "' style='width:100%; float:right; text-align:right; padding-right:50px;'>\r\n" +
                        "   </div>\r\n" +
                        "</div>\r\n";
                }
                RetAjaxOut.HtmlContent = WebEnvironments.ProcessHtml(html);
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DrawReadUser(RenderInfoCls ORenderInfo, string userId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                string userLocalLoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                var ou = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, userId);
                if (ou != null)
                {
                    string avatar = null;
                    string hoTen = ou.FullName;
                    BacSyOwnerUserCls[] bacSyOwnerUsers = CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Reading(ORenderInfo, new BacSyOwnerUserFilterCls() { OWNERUSERID = ou.OwnerUserId });
                    foreach (var bacSyOwnerUser in bacSyOwnerUsers)
                    {
                        BacSyCls bacSy = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, bacSyOwnerUser.BACSYID);
                        if (bacSy != null)
                        {
                            avatar = bacSy.NDANH;
                            hoTen = bacSy.HOTEN;
                            break;
                        }
                    }
                    string html = string.IsNullOrEmpty(avatar) ? "<i title='" + hoTen + "' class='fa fa-user img-circle' style='font-size:25px;'> </i>\r\n" : "<img title='" + hoTen + "' alt=\"image\" style=\"width:20px;height:20px\" class=\"img-circle\" src='" + avatar + "' />\r\n";
                    RetAjaxOut.HtmlContent = WebEnvironments.ProcessHtml(html);
                }
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut PopupInviteJoinRoom(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                var lichHoiChans = CallBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().Reading(ORenderInfo, new LichHoiChanFilterCls() { });
                string cbbLichHoiChan = "<select id = 'cbbLichHoiChan' title = 'Lịch hội chẩn' class='form-control width100' onchange='cbbLichHoiChan_onchange(this)'>\r\n";
                cbbLichHoiChan += "<option value=''>" + WebLanguage.GetLanguage(OSiteParam, "Lịch hội chẩn") + "</option>\r\n";
                foreach (var lichHoiChan in lichHoiChans)
                {
                    cbbLichHoiChan += string.Format("<option {0} value={1}>" + WebLanguage.GetLanguage(OSiteParam, "Lịch hội chẩn:") + " {2} - {3}</option>\r\n", null, lichHoiChan.ID, lichHoiChan.BATDAU.Value.ToString("HH:mm dd/MM/yyyy"), lichHoiChan.DIADIEM);
                }
                cbbLichHoiChan += "</select>\r\n";
                WebSessionUtility.SetSession(OSiteParam, "RoomMembers", new List<RoomMember>());

                string html =
                "<form action='javascript:InviteJoinRoom();' autocomplete='off'> \r\n" +
                    "<div style='position: absolute; top:0px; right:5px; bottom:40px; left:5px; overflow-y:auto;'>\r\n" +
                        "<div class=\"form-group\">" + cbbLichHoiChan + "</div>" +
                        "<div id = 'divRoomMemberList' class=\"form-group\">" + DrawRoomMemberList(ORenderInfo).HtmlContent + "</div>" +
                    "</div>\r\n" +
                    "<div style='position: absolute; bottom:0px; right:0px;' class='popupButton'>\r\n" +
                        "   <input type='button' class='popupClose' value='" + WebLanguage.GetLanguage(OSiteParam, "Hủy") + "' onclick='javascript:$(\"#divFormModal\").modal(\"hide\");' style='float:right'> \r\n" +
                        "   <input type='submit' class='popupAdd' value='" + WebLanguage.GetLanguage(OSiteParam, "Mời") + "' style='float:right'> \r\n" +
                    "</div>\r\n" +
                "</form>\r\n";
                RetAjaxOut.HtmlContent = WebEnvironments.ProcessHtml(html);
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static string CbbBacSy(RenderInfoCls ORenderInfo, int? id, string value)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            string cbbThanhVienHoiChan =
                string.Format("<select id = 'cbbBacSy{0}' title = '" + WebLanguage.GetLanguage(OSiteParam, "--chọn--") + "' style='display:{1}' {2} onchange='cbbBacSy_OnChange(this, {0});'>\r\n", id, id != null ? "none" : "block", id != null ? "class='CssEditorItemThanhVien'" : null);
            if (!string.IsNullOrEmpty(value))
            {
                BacSyCls thanhVien = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, value);
                if (thanhVien != null)
                    cbbThanhVienHoiChan += string.Format("<option value={0}>{1} - {2}</option>\r\n", thanhVien.ID, thanhVien.MA, thanhVien.HOTEN);
                else
                    cbbThanhVienHoiChan += string.Format("<option value={0}> " + WebLanguage.GetLanguage(OSiteParam, "Không xác định") + "</option>\r\n", value);
            }
            else cbbThanhVienHoiChan += "<option value=''> " + WebLanguage.GetLanguage(OSiteParam, "--chọn--") + "</option>\r\n";
            cbbThanhVienHoiChan += "</select>\r\n";
            return cbbThanhVienHoiChan;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DrawRoomMemberList(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<RoomMember> roomMembers = WebSessionUtility.GetSession(OSiteParam, "RoomMembers") as List<RoomMember>;
                string html =
                      "<table class=\"table table-striped\"> \r\n" +
                          "<thead> \r\n" +
                              "<tr> \r\n" +
                                 "<th width=30>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                                 "<th width=40%>" + WebLanguage.GetLanguage(OSiteParam, "Họ tên") + " </th> \r\n" +
                                 "<th>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                                 "<th>" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị") + " </th> \r\n" +
                                 "<th>" + WebLanguage.GetLanguage(OSiteParam, "Tên đăng nhập") + " </th> \r\n" +
                                 "<th width=60 style='text-align:center;'><a href='javascript:ShowAddRoomMember()' title= '" + WebLanguage.GetLanguage(OSiteParam, "Thêm thành viên") + "'><i class='fa fa-plus' style='font-size:20px;'></i></a></th> \r\n" +
                              "</tr> \r\n" +
                          "</thead> \r\n" +
                          "<tbody> \r\n" +
                              "<tr id='trAddThanhVien' style='display:none;'> \r\n" +
                                  "<input type='hidden' id='hdChuyenGiaHoiChanId' value=''>\r\n" +
                                  "<input type='hidden' id='hdBacSyFullName' value=''>\r\n" +
                                  "<td></td> \r\n" +
                                  "<td><div id='divCbbBacSy'>" + CbbBacSy(ORenderInfo, null, null) + "</div></td> \r\n" +
                                  "<td><span id='spMa'></span></td> \r\n" +
                                  "<td><span id='spDonVi'></span></td> \r\n" +
                                  "<td><span id='spLoginName'></span></td> \r\n" +
                                  "<td style='text-align:center;'><a href='javascript:SaveRoomMember(\"\");' title= '" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "'><i class='fa fa-save' style='font-size:20px; margin-top:4px;'></i><span class='text-muted'></span></a></td> \r\n" +
                              "</tr> \r\n";
                for (int iIndex = 0; iIndex < roomMembers.Count; iIndex++)
                {
                    html += "<tr> \r\n" +
                                "<input type='hidden' id='hdChuyenGiaHoiChanId" + iIndex + "' value='" + roomMembers[iIndex].id + "'>\r\n" +
                                "<input type='hidden' id='hdBacSyId" + iIndex + "' value='" + roomMembers[iIndex].bacSyId + "'>\r\n" +
                                "<input type='hidden' id='hdBacSyFullName" + iIndex + "' value='" + roomMembers[iIndex].bacSyFullName + "'>\r\n" +
                                "<td>" + (iIndex + 1) + "</td> \r\n" +
                                "<td><div id='divCbbBacSy" + iIndex + "' class='CssEditorItemThanhVien' style='display:none'></div><span class='CssDisplayItemThanhVien' id='spThanhVienHoiChan" + iIndex + "'>" + roomMembers[iIndex].bacSyFullName + "</span></td> \r\n" +
                                "<td><span id='spMa" + iIndex + "'>" + roomMembers[iIndex].bacSyCode + "</span></td> \r\n" +
                                "<td><span id='spDonVi" + iIndex + "'>" + roomMembers[iIndex].donViCongTac + "</span></td> \r\n" +
                                "<td><span id='spLoginName" + iIndex + "'>" + roomMembers[iIndex].loginName + "</span></td> \r\n" +
                                "<td style='text-align:center;'>\r\n" +
                                    "<a id='btnDeleteThanhVien" + iIndex + "' class='CssDisplayItemThanhVien' href='javascript:DeleteRoomMember(" + iIndex + ");' title= '" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "'><i class='fa fa-times' style='font-size:20px;'></i></a>\r\n" +
                                "</td> \r\n" +
                            "</tr> \r\n";
                }
                html += "</tbody> \r\n" +
                    "</table> \r\n";
                RetAjaxOut.HtmlContent = WebEnvironments.ProcessHtml(html);
            }
            catch (Exception ex)
            {
            }
            return RetAjaxOut;
        }
        #endregion
        #region Xử lý nghiệp vụ
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut CheckGiangVien(RenderInfoCls ORenderInfo, string khoaHocId, string userName)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                DT_LichLyThuyetChiTietCls lichLyThuyetChiTiet = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().Reading(ORenderInfo, new DT_LichLyThuyetChiTietFilterCls() { LICHLYTHUYET_ID = khoaHocId, NGAY = DateTime.Today }).Where(o => o.THOIGIAN == null || o.THOIGIAN < DateTime.Now).OrderByDescending(o => o.THOIGIAN).FirstOrDefault();

                BacSyCls giangVien = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichLyThuyetChiTiet.GIANGVIEN_ID);

                List<string> giangVienUserIds = CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Reading(ORenderInfo, new BacSyOwnerUserFilterCls() { BACSYID = lichLyThuyetChiTiet.GIANGVIEN_ID }).Select(o => o.OWNERUSERID).ToList();
                foreach (string giangVienUserId in giangVienUserIds)
                {
                    var us = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, giangVienUserId);
                    if (us != null && us.LoginName == userName)
                    {
                        RetAjaxOut.RetObject = true;
                        return RetAjaxOut;
                    }
                }
                RetAjaxOut.RetObject = false;
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut UpdateConvId(RenderInfoCls ORenderInfo, string khoaHocId, string convId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KhoaHocCls khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
                if (khoaHoc == null)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "khóa học này đã bị xóa bởi người dùng khác.");
                    return RetAjaxOut;
                }
                khoaHoc.CONVID = convId;
                CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Save(ORenderInfo, khoaHoc.ID, khoaHoc);
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut GetConvId(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KhoaHocCls khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
                RetAjaxOut.RetExtraParam1 = khoaHoc.CONVID;
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut AddUser(RenderInfoCls ORenderInfo, string ToUser, string call_ID, string LoginName)
        {
            string jwtToken = CreateJWT(ORenderInfo).RetExtraParam1;
            AjaxOut RetAjaxOut = new AjaxOut();
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            WebSession.CheckSessionTimeOut(ORenderInfo);
            List<RoomMember> roomMembers = WebSessionUtility.GetSession(OSiteParam, "RoomMembers") as List<RoomMember>;
            var lstInvitedUser = roomMembers.Select(o => o.loginName).ToList();
            foreach (var InvitedUser in lstInvitedUser)
            {
                if (InvitedUser != ToUser)
                {
                    var obj = new
                    {
                        callId = call_ID,
                        from = new { type = "internal", number = LoginName, alias = LoginName },
                        to = new { type = "internal", number = InvitedUser, alias = InvitedUser },
                        spyCall = false
                    };
                    var data = JsonConvert.SerializeObject(obj);
                    string URL = WebConfig.GetWebConfig("AddUserURL");
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                    request.Method = "POST";
                    request.Headers.Add("X-STRINGEE-AUTH", jwtToken);
                    request.ContentType = "application/json";
                    request.ContentLength = data.Length;
                    StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
                    requestWriter.Write(data);
                    requestWriter.Close();
                    try
                    {
                        WebResponse webResponse = request.GetResponse();
                        Stream webStream = webResponse.GetResponseStream();
                        StreamReader responseReader = new StreamReader(webStream);
                        string response = responseReader.ReadToEnd();
                        responseReader.Close();
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            return RetAjaxOut;
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut SetVideoFloor(RenderInfoCls ORenderInfo, string User, string call_ID)
        {
            string jwtToken = CreateJWT(ORenderInfo).RetExtraParam1;
            AjaxOut RetAjaxOut = new AjaxOut();
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            WebSession.CheckSessionTimeOut(ORenderInfo);
            var obj_1 = new
            {
                callId = call_ID,
                userId = User,
            };
            var data1 = JsonConvert.SerializeObject(obj_1);
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create("https://api.stringee.com/v1/call2/setvideofloor");
            request1.Method = "POST";
            request1.Headers.Add("X-STRINGEE-AUTH", jwtToken);
            request1.ContentType = "application/json";
            request1.ContentLength = data1.Length;
            StreamWriter requestWriter1 = new StreamWriter(request1.GetRequestStream(), System.Text.Encoding.ASCII);
            requestWriter1.Write(data1);
            requestWriter1.Close();
            try
            {
                WebResponse webResponse = request1.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                responseReader.Close();
            }
            catch (Exception e)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = e.Message;
            }
            return RetAjaxOut;
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut GetThanhVienHoiChans(RenderInfoCls ORenderInfo, string lichHoiChanId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                LichHoiChanCls lichHoiChan = string.IsNullOrEmpty(lichHoiChanId) ? null : CallBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().CreateModel(ORenderInfo, lichHoiChanId);
                if (lichHoiChan == null)
                {
                    RetAjaxOut.HtmlContent = WebLanguage.GetLanguage(OSiteParam, "Lịch hội chẩn vừa chọn không tồn tại.");
                }
                else
                {
                    var currentUser = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                    LapLichThanhVienHoiChanCls[] lapLichThanhVienHoiChans = CallBussinessUtility.CreateBussinessProcess().CreateLapLichThanhVienHoiChanProcess().Reading(ORenderInfo, new LapLichThanhVienHoiChanFilterCls() { LICHHOICHANID = lichHoiChanId });
                    List<RoomMember> roomMembers = new List<RoomMember>();
                    foreach (var lapLichThanhVienHoiChan in lapLichThanhVienHoiChans)
                    {
                        BacSyOwnerUserCls[] bacSyOwnerUsers = CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Reading(ORenderInfo, new BacSyOwnerUserFilterCls() { BACSYID = lapLichThanhVienHoiChan.BACSYID });
                        OwnerUserCls ownerUser = null;
                        foreach (BacSyOwnerUserCls bacSyOwnerUser in bacSyOwnerUsers)
                        {
                            ownerUser = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, bacSyOwnerUser.OWNERUSERID);
                            if (ownerUser != null && ownerUser.LoginName != currentUser.LoginName)
                            {
                                BacSyCls bacSy = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lapLichThanhVienHoiChan.BACSYID);
                                roomMembers.Add(new RoomMember()
                                {
                                    id = lapLichThanhVienHoiChan.ID,
                                    ownerUserId = ownerUser.OwnerUserId,
                                    bacSyId = lapLichThanhVienHoiChan.BACSYID,
                                    loginName = ownerUser.LoginName,
                                    bacSyCode = bacSy != null ? bacSy.MA : null,
                                    bacSyFullName = bacSy != null ? bacSy.HOTEN : null,
                                    donViCongTac = lapLichThanhVienHoiChan.DONVICONGTAC
                                });
                                break;
                            }
                        }
                    }
                    WebSessionUtility.SetSession(OSiteParam, "RoomMembers", roomMembers);
                    RetAjaxOut.HtmlContent = DrawRoomMemberList(ORenderInfo).HtmlContent;
                }
            }
            catch (Exception ex)
            {
            }
            return RetAjaxOut;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut GetThongTinBacSy(RenderInfoCls ORenderInfo, string bacSyId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                BacSyCls bacSy = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, bacSyId);
                if (bacSy != null)
                {
                    var donVi = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDonViCongTacProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), bacSy.DONVIMA);                  
                    BacSyOwnerUserCls[] bacSyOwnerUsers = CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Reading(ORenderInfo, new BacSyOwnerUserFilterCls() { BACSYID = bacSyId });
                    OwnerUserCls ownerUser = null;
                    foreach (BacSyOwnerUserCls bacSyOwnerUser in bacSyOwnerUsers)
                    {
                        ownerUser = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, bacSyOwnerUser.OWNERUSERID);
                        if (ownerUser != null)
                            break;
                    }
                    RetAjaxOut.RetExtraParam6 = Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        bacSyCode = bacSy.MA,
                        bacSyFullName = bacSy.HOTEN,
                        donViCongTac = donVi != null ? donVi.Ten : null,
                        loginName = ownerUser != null ? ownerUser.LoginName : null
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return RetAjaxOut;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut SaveRoomMember(RenderInfoCls ORenderInfo, string chuyenGiaHoiChanId, string bacSyId, string ma, string bacSyFullName, string donVi, string loginName)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<RoomMember> roomMembers = WebSessionUtility.GetSession(OSiteParam, "RoomMembers") as List<RoomMember>;
                if (roomMembers.Any(o => o.bacSyId == bacSyId && o.id != chuyenGiaHoiChanId))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Thành viên này đã có trong danh sách.\nXin chọn thành viên khác.");
                    return RetAjaxOut;
                }
                if (loginName == WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Bạn không thể chọn chính mình.\nXin chọn thành viên khác.");
                    return RetAjaxOut;
                }
                if (string.IsNullOrEmpty(chuyenGiaHoiChanId))//Thêm mới
                {
                    RoomMember roomMember = new RoomMember();
                    roomMember.id = System.Guid.NewGuid().ToString();
                    roomMember.bacSyId = bacSyId;
                    roomMember.bacSyCode = ma;
                    roomMember.bacSyFullName = bacSyFullName;
                    roomMember.donViCongTac = donVi;
                    roomMember.loginName = loginName;
                    roomMembers.Add(roomMember);
                }
                else
                {
                    RoomMember roomMember = roomMembers.Where(o => o.id == chuyenGiaHoiChanId).FirstOrDefault();
                    if (roomMember.Equals(null))
                    {
                        RetAjaxOut.Error = true;
                        RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Thành viên này đã bị xóa bởi người dùng khác.");
                        return RetAjaxOut;
                    }
                    roomMember.bacSyId = bacSyId;
                    roomMember.bacSyCode = ma;
                    roomMember.bacSyFullName = bacSyFullName;
                    roomMember.donViCongTac = donVi;
                    roomMember.loginName = loginName;
                }
                RetAjaxOut.HtmlContent = DrawRoomMemberList(ORenderInfo).HtmlContent;
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DeleteRoomMember(RenderInfoCls ORenderInfo, string chuyenGiaHoiChanId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<RoomMember> roomMembers = WebSessionUtility.GetSession(OSiteParam, "RoomMembers") as List<RoomMember>;
                RoomMember roomMember = roomMembers.Where(o => o.id == chuyenGiaHoiChanId).FirstOrDefault();
                if (roomMember.Equals(null))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Thành viên này đã bị xóa bởi người dùng khác.");
                    return RetAjaxOut;
                }
                roomMembers.Remove(roomMember);
                RetAjaxOut.HtmlContent = DrawRoomMemberList(ORenderInfo).HtmlContent;
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static List<string> GetLoginNames(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<RoomMember> roomMembers = WebSessionUtility.GetSession(OSiteParam, "RoomMembers") as List<RoomMember>;
                return roomMembers.Select(o => o.loginName).ToList();
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return new List<string>();
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut CreateJWT(RenderInfoCls ORenderInfo)//Tạo JSON web token
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                string apiKeySid = WebConfig.GetWebConfig("ApiKeySid");
                string apiKeySecret = WebConfig.GetWebConfig("ApiKeySecret");
                var payload = new
                {
                    jti = apiKeySid + "_" + ConvertToTimestamp(DateTime.Now),
                    iss = apiKeySid,
                    exp = ConvertToTimestamp(DateTime.Now.AddHours(8)),
                    rest_api = true
                };

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

                Dictionary<string, object> extraHeaders = new Dictionary<string, object>();
                extraHeaders.Add("cty", "onenet");
                extraHeaders.Add("typ", "JWT");
                extraHeaders.Add("alg", "HS256");

               // string token = Jose.JWT.Encode(json, System.Text.Encoding.UTF8.GetBytes(apiKeySecret), Jose.JwsAlgorithm.HS256, extraHeaders);
               // RetAjaxOut.RetExtraParam1 = token;
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        /// <summary>
        /// method for converting a System.DateTime value to a UNIX Timestamp
        /// </summary>
        /// <param name="value">date to convert/// <returns></returns>
        private static double ConvertToTimestamp(DateTime value)
        {
            //the Unix Epoch
            //TimeSpan span = value.TimeOfDay;
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            //return the total seconds (which is a UNIX timestamp)
            return (double)span.TotalSeconds;
        }
        #endregion
    }

}
