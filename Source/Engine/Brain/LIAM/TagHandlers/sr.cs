using System;
using System.Xml;
using System.Text;

namespace LIAM.AIMLTagHandlers
{
    /// <summary>
    /// The sr element is a shortcut for: 
    /// 
    /// <srai><star/></srai> 
    /// 
    /// The atomic sr does not have any content. 
    /// </summary>
    public class sr : LIAM.Utils.AIMLTagHandler
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="bot">The bot involved in this request</param>
        /// <param name="user">The user making the request</param>
        /// <param name="query">The query that originated this node</param>
        /// <param name="request">The request inputted into the system</param>
        /// <param name="result">The result to be passed to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public sr(LIAM.Bot bot,
                        LIAM.User user,
                        LIAM.Utils.SubQuery query,
                        LIAM.Request request,
                        LIAM.Result result,
                        XmlNode templateNode)
            : base(bot, user, query, request, result, templateNode)
        {
        }

        protected override string ProcessChange()
        {
            if (this.templateNode.Name.ToLower() == "sr")
            {
                XmlNode starNode = Utils.AIMLTagHandler.getNode("<star/>");
                star recursiveStar = new star(this.bot, this.user, this.query, this.request, this.result, starNode);
                string starContent = recursiveStar.Transform();

                XmlNode sraiNode = LIAM.Utils.AIMLTagHandler.getNode("<srai>"+starContent+"</srai>");
                srai sraiHandler = new srai(this.bot, this.user, this.query, this.request, this.result, sraiNode);
                return sraiHandler.Transform();
            }
            return string.Empty;
        }
    }
}
