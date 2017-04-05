using System;

namespace SphereClient.Entities {
    [Serializable]
    public abstract class Entity {
        public int Id { get; set; }

        private bool isnull = false;
        public bool IsNull { get { return isnull; } set { isnull = value; } }

        public string ToJSON() {
            return JSON.Stringify(this);
        }

        /// <summary>
        /// Textual representation of the description of the object.
        /// Unlike <see cref="Entity.ToString()"/>, this method is 
        /// used specially when filling an <see cref="ImageAndTitleListPane"/>
        /// object, using the returned value of this method as the text beside
        /// the image.
        /// </summary>
        /// <returns>the text description of the object</returns>
        public virtual string ToText() {
            return "Default Entity representation text";
        }
    }
}
