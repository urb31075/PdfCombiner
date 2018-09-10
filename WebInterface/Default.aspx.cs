using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebInterface
{
    using System.Collections.Specialized;

    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                var y = SessionHelper.Get<int>(this.Session, SessionHelper.SessionKey.key1);
            }
            else
            {
                SessionHelper.Set(this.Session, SessionHelper.SessionKey.key1, 25);
                var x = new WebInterfaceEntities();
                //var m = new Main { Name = "aaa", Comment = "aaa111", Amount = 10 };
                //x.Main.Add(m);
                //x.SaveChanges();
                var s = x.Main.ToList();
            }
            return;
        }

        protected string CommandLineParameter
        {
            get
            {
                var s = string.Empty;
                foreach (var item in this.Request.QueryString)
                {
                    s += item + "=" + this.Request.QueryString[item.ToString()] + " ";
                }

                return s.Trim();
            }
        }

        protected string RouteParameter
        {
            get
            {
                var s = string.Empty;
                foreach (var item in this.RouteData.Values)
                {
                    s += item.Key + "=" + item.Value + " ";
                }

                return s.Trim();
            }
        }

        protected IEnumerable<string> GetMain()
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var c in new WebInterfaceEntities().Main)
            {
                yield return $"{c.Id} {c.Name} {c.Comment} {c.Amount??0}";
            }

         //  return x.Main.Select(c => $"{c.Id} {c.Name??"noname"} {c.Comment??"nocomment"} {c.Amount??0}").ToList();
        }
    }
}