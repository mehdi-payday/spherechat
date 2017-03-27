using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SphereCliet {
    class FeedBox : Panel{

        public IList<Message> contents;


        public FeedBox() {
            this.AutoScroll = true;
            this.VScroll = true;
            this.HScroll = false;
            this.contents = new List<Message>();
        }

        public void Addmessage(Message msg) {
            this.contents.Add( msg );
            
        }
        

    }
}
