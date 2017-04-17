using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SphereClient {
    public sealed class Constants {
        //COLORS
        public static readonly Color DARK_PURPLE = Color.FromArgb( 62, 1, 56 );
        public static readonly Color PURPLE = Color.FromArgb( 93, 6, 61 );
        public static readonly Color LIGHT_PURPLE = Color.FromArgb( 165, 56, 202 );//165, 97, 149 );
        public static readonly Color LIGHT_GRAY = Color.FromArgb( 243, 249, 242 );
        public static readonly Color DARK_GRAY = Color.FromKnownColor( KnownColor.Desktop );
        public static readonly Color GRAY = Color.Gray;
        public static readonly Color BASE_WHITE = Color.FromArgb(245,245,245);
        public static readonly Color BLUE = Color.FromArgb( 63, 153, 231 );
        public static readonly Color RED = Color.Red;
        
        //IMAGES
        public static readonly Image TRANSPARENT_GRADIENT_VERTICAL = Properties.Resources.Untitled;
        public static readonly Image LOGO = Properties.Resources.path4149;

        //SIZES
        public static readonly Size MEDIUM_IMAGE_SIZE = new Size( 40, 40 );

        //MARGINS
        public static readonly Padding MARGIN_SMALL = new Padding( 5 );

        //FONTS
        public static readonly Font BIG_BOLD_FONT = new Font( "Microsoft Sans Serif", 12.0f, FontStyle.Bold, GraphicsUnit.Point );
        public static readonly Font SECTION_TITLE_FONT = new Font( "Microsoft Sans Serif", 14.25f, FontStyle.Bold, GraphicsUnit.Point );

        //MESSAGES
        public static readonly int MAX_MESSAGE_PANE_WIDTH = 500;

        //URLS
        public static readonly string USER_NOIMAGE_URL = "https://help.sketchbook.com/knowledgebase/wp-content/plugins/all-in-one-seo-pack/images/default-user-image.png";


    }
}
