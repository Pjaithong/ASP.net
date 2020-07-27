using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TpPizzas01;
using TpPizzas01.Models;

namespace TpPizzas01.Annotation
{
    public static class CheckerExtension
    {
        public static bool Validate(this object item, ModelStateDictionary modelState)
        {
            bool result = true;

            PropertyInfo[] properties = item.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                foreach (var checkerAttribut in property.GetCustomAttributes<Checker>())
                {
                    //excecuter la fct checker 
                    result = checkerAttribut.Validate(item, property, result, modelState);
                }
            }

            return result;
        }

        public static bool Validate(this object item, ModelStateDictionary modelState, List<Pizza> pizzas)
        {
            bool result = true;

            PropertyInfo[] properties = item.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                foreach (var checkerAttribut in property.GetCustomAttributes<Checker>())
                {
                    result = checkerAttribut.Validate(item, property, result, modelState, pizzas);
                }
            }

            return result;
        }
    }

    
}