using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace TpPizzas01.Annotation
{
    //cette annotation pour pouvoir utiliser l'annotation checker
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class Checker : Attribute
    {
        public enum CheckerAction
        {
            Length,
            Required,
            DatasOccurs,
            NotSame
        }
        private String[] propretiesName; //propriete qu'on a besoin
        private CheckerAction[] actions; //mode à verifier, plus infos 
        public int LengthMin { get; set; } = 0;
        public int LengthMax { get; set; } = 0;
        public int OccurMin { get; set; } = 0;
        public int OccurMax { get; set; } = 0;

        public Checker(String[] propretiesName, params CheckerAction[] actions )
        {
            this.propretiesName = propretiesName;
            this.actions = actions;

        }

        internal bool Validate(object item, PropertyInfo property, bool result, ModelStateDictionary modelState)
        {
            if (actions.Contains(CheckerAction.Length)
            {

            }


            throw new NotImplementedException();
        }

        private bool Length(object item, PropertyInfo)
        internal bool Validate(object item, PropertyInfo property, bool result, ModelStateDictionary modelState, List<Models.Pizza> pizzas)
        {
            throw new NotImplementedException();
        }
    }
}