using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Model
{
    class ResponseRecipef2f
    {
        private String publisher;
        private double socialRank;
        private String recipeId;
        private String f2fUrl;
        private String imageUrl;
        private String publisherUrl;
        private String title;
        private String sourceUrl;

        public string Publisher
        {
            get
            {
                return publisher;
            }

            set
            {
                publisher = value;
            }
        }

        public double SocialRank
        {
            get
            {
                return socialRank;
            }

            set
            {
                socialRank = value;
            }
        }

        public String RecipeId
        {
            get
            {
                return recipeId;
            }

            set
            {
                recipeId = value;
            }
        }

        public string F2fUrl
        {
            get
            {
                return f2fUrl;
            }

            set
            {
                f2fUrl = value;
            }
        }

        public string PublisherUrl
        {
            get
            {
                return publisherUrl;
            }

            set
            {
                publisherUrl = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public string SourceUrl
        {
            get
            {
                return sourceUrl;
            }

            set
            {
                sourceUrl = value;
            }
        }

        public string ImageUrl
        {
            get
            {
                return imageUrl;
            }

            set
            {
                imageUrl = value;
            }
        }
    }


}
