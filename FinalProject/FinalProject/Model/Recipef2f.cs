﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Model
{
    class Recipef2f
    {
        private String imageUrl; // URL of the image
        private String sourceUrl; // Original Url of the recipe on the publisher's site
        private int recipeId; // Id used for follow-up request
        private String f2fUrl; // Url of the recipe on Food2Fork.com
        private String title; // Title of the recipe
        private String publisher; // Name of the Publisher
        private String publisherUrl; // Base url of the publisher
        private String socialRank; // The Social Ranking of the Recipe (As determined by f2f Ranking Algorithm)
        private int page; // The page number that is being returned (To keep track of concurrent requests)

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

        public int RecipeId
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

        public string SocialRank
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

        public int Page
        {
            get
            {
                return page;
            }

            set
            {
                page = value;
            }
        }
    }
}
