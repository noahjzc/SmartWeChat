using System.Collections.Generic;
using System.Text;

namespace SmartWeChat.DTO.Message
{
    public abstract class ReplyMessageModelBase : MessageBase
    {
        protected abstract string BuildSubXml();

        public override string ToString()
        {
            StringBuilder rtn = new StringBuilder();
            rtn.Append("<xml>");
            rtn.Append($"<ToUserName><![CDATA[{ToUserName}]]></ToUserName>");
            rtn.Append($"<FromUserName><![CDATA[{FromUserName}]]></FromUserName>");
            rtn.Append($"<CreateTime>{CreateTime}</CreateTime>");
            rtn.Append($"<MsgType><![CDATA[{MsgType}]]></MsgType>");
            rtn.Append(BuildSubXml());
            rtn.Append("</xml>");
            return rtn.ToString();
        }
    }

    public class ReplyTextMessageModel : ReplyMessageModelBase
    {
        public string Content { get; set; }

        protected override string BuildSubXml()
        {
            return $"<Content><![CDATA[{Content}]]></Content>";
        }
    }

    public abstract class ReplyMediaMessageModel : ReplyMessageModelBase
    {
        public string MediaId { get; set; }
    }

    public class ReplyImageMessageModel : ReplyMediaMessageModel
    {
        protected override string BuildSubXml()
        {
            return $"<Image><MediaId><![CDATA[{MediaId}]]></MediaId></Image>";
        }
    }

    public class ReplyVoiceMessageModel : ReplyMediaMessageModel
    {
        protected override string BuildSubXml()
        {
            return $"<Voice><MediaId><![CDATA[{MediaId}]]></MediaId></Voice>";
        }
    }

    public class ReplyVideoMessageModel : ReplyMediaMessageModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        protected override string BuildSubXml()
        {
            string rtn = "<Video>";
            rtn += $"<MediaId><![CDATA[{MediaId}]]></MediaId>";
            if (!string.IsNullOrEmpty(Title)) rtn += $"<Title><![CDATA[{Title}]]></Title>";
            if (!string.IsNullOrEmpty(Description)) rtn += $"<Description><![CDATA[{Description}]]></Description>";
            rtn += "</Video>";

            return rtn;
        }
    }

    public class ReplyMusicMessageModel : ReplyMessageModelBase
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string MusicUrl { get; set; }

        public string HQMusicUrl { get; set; }

        public string ThumbMediaId { get; set; }

        protected override string BuildSubXml()
        {
            string rtn = "<Music>";
            rtn += $"<ThumbMediaId><![CDATA[{ThumbMediaId}]]></ThumbMediaId>";
            if (!string.IsNullOrEmpty(Title)) rtn += $"<Title><![CDATA[{Title}]]></Title>";
            if (!string.IsNullOrEmpty(Description)) rtn += $"<Description><![CDATA[{Description}]]></Description>";
            if (!string.IsNullOrEmpty(MusicUrl)) rtn += $"<MusicUrl><![CDATA[{MusicUrl}]]></MusicUrl>";
            if (!string.IsNullOrEmpty(HQMusicUrl)) rtn += $"<HQMusicUrl><![CDATA[{HQMusicUrl}]]></HQMusicUrl>";
            rtn += "</Music>";

            return rtn;
        }
    }

    public class ReplyArticleMessageModel : ReplyMessageModelBase
    {
        public int ArticleCount { get; set; }

        public IEnumerable<ArticleModel> Articles { get; set; }

        public class ArticleModel
        {
            public string Title { get; set; }

            public string Description { get; set; }

            public string PicUrl { get; set; }

            public string Url { get; set; }

            public override string ToString()
            {
                string rtn = "<item>";
                rtn += $"<Title><![CDATA[{Title}]]></Title>";
                rtn += $"<Description><![CDATA[{Description}]]></Description>";
                rtn += $"<PicUrl><![CDATA[{PicUrl}]]></PicUrl>";
                rtn += $"<Url><![CDATA[{Url}]]></Url>";
                rtn += "</item>";

                return rtn;
            }
        }

        protected override string BuildSubXml()
        {
            string rtn = $"<ArticleCount>{ArticleCount}</ArticleCount>";
            rtn += "<Articles>";
            foreach (var article in Articles)
            {
                rtn += article.ToString();
            }
            rtn += "</Articles>";

            return rtn;
        }
    }
}
