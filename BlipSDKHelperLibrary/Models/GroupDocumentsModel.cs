using Lime.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlipSDKHelperLibrary.Models
{
    public class GroupDocumentsModel
    {
        internal List<DocumentModel> Docs { get; set; }
        
        internal DocumentModel GetDoc(int index)
        {
            return Docs[index];
        }
        public GroupDocumentsModel()
        {
            Docs = new List<DocumentModel>();
        }

        public void Add(Document doc, int order = 0)
        {
            Docs.Add(new DocumentModel(doc,order));
        }
       
        internal class DocumentModel
        {
            public Document Doc { get; set; }
            public int Order { get; set; }

            internal DocumentModel(Document document, int order = 0)
            {
                Doc = document;
                Order = order;
            }
        }
    }
}
