using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Model
{
    class Responsef2f
    {
        private int count;
        private List<ResponseRecipef2f> recipe; //Informations about the recipe

        public int Count
        {
            get
            {
                return count;
            }

            set
            {
                count = value;
            }
        }

        internal List<ResponseRecipef2f> Recipe
        {
            get
            {
                return recipe;
            }

            set
            {
                recipe = value;
            }
        }
    }
}
