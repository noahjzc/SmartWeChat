using System.Xml.Serialization;

namespace SmartWeChat.DTO.Message
{
    public class HandleMessageModelBase : MessageBase
    {
        public long MsgId { get; set; }

        public string Encrypt { get; set; }
    }

    public class HandleTextMessageModel : HandleMessageModelBase
    {
        public string Content { get; set; }
    }

    public class HandleMediaMessageModel : HandleMessageModelBase
    {
        public string MediaId { get; set; }
    }

    public class HandleImageMessageModel : HandleMediaMessageModel
    {
        public string PicUrl { get; set; }
    }

    public class HandleVoiceMessageModel : HandleMediaMessageModel
    {
        public string Format { get; set; }

        public string Recognition { get; set; }
    }

    public class HandleVideoMessageModel : HandleMediaMessageModel
    {
        public string ThumbMediaId { get; set; }
    }

    public class HandleLocationMessageModel : HandleMessageModelBase
    {
        public double Location_X { get; set; }

        public double Location_Y { get; set; }

        public double Scale { get; set; }

        public string Label { get; set; }
    }

    public class HandleLinkMessageModel : HandleMessageModelBase
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }
    }

    public class HandleEventMessageModelBase : MessageBase
    {
        public string Event { get; set; }
    }

    public class HandleSubscribeEventModel : HandleEventMessageModelBase
    {
        public string EventKey { get; set; }

        public string Ticket { get; set; }

        [XmlIgnore]
        public bool IsScan => !string.IsNullOrEmpty(EventKey) && EventKey.StartsWith("qrscene_");
    }

    public class HandleQRCodeScanEventModel : HandleEventMessageModelBase
    {
        public string EventKey { get; set; }

        public string Ticket { get; set; }
    }

    public class HandleLocationEventModel : HandleEventMessageModelBase
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Precision { get; set; }
    }

    public class HandleMenuClickedEventModel : HandleEventMessageModelBase
    {
        public string EventKey { get; set; }
    }
}
