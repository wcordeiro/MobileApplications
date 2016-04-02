using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Model
{
    class ResponseYummly
    {
        private String imageUrl;
        private String sourceDisplayName;
        private String ingredients;
        private String id;
        private String recipeName;
        private Double totalTime;
        private String course;
        private String cuisine;
        private String flavors;
        private Double rating;

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

        public string SourceDisplayName
        {
            get
            {
                return sourceDisplayName;
            }

            set
            {
                sourceDisplayName = value;
            }
        }

        public string Ingredients
        {
            get
            {
                return ingredients;
            }

            set
            {
                ingredients = value;
            }
        }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string RecipeName
        {
            get
            {
                return recipeName;
            }

            set
            {
                recipeName = value;
            }
        }

        public double TotalTime
        {
            get
            {
                return totalTime;
            }

            set
            {
                totalTime = value;
            }
        }

        public string Course
        {
            get
            {
                return course;
            }

            set
            {
                course = value;
            }
        }

        public string Cuisine
        {
            get
            {
                return cuisine;
            }

            set
            {
                cuisine = value;
            }
        }

        public string Flavors
        {
            get
            {
                return flavors;
            }

            set
            {
                flavors = value;
            }
        }

        public double Rating
        {
            get
            {
                return rating;
            }

            set
            {
                rating = value;
            }
        }
    }
}
