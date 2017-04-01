using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MvcMate.Web.Mvc.Html
{
    /// <summary>
    /// Represents support for HTML input "file" controls in an application.
    /// </summary>
    public static class UploadExtensions
    {
        /// <summary>
        /// Returns a file input element by using the specified HTML helper and the name of the form field.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field.</param>
        /// <returns>An input element whose type attribute is set to "file".</returns>
        public static HtmlString Upload(this HtmlHelper helper, string name)
        {
            if (helper == null)
                throw new ArgumentNullException("helper");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Target cannot be null or empty", "name");
            Contract.EndContractBlock();

            return Upload(helper, name, null);
        }

        /// <summary>
        /// Returns a file input element by using the specified HTML helper, the name of the form field, and the HTML attributes.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An input element whose type attribute is set to "file".</returns>
        public static HtmlString Upload(this HtmlHelper helper, string name, object htmlAttributes)
        {
            if (helper == null)
                throw new ArgumentNullException("helper");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Target cannot be null or empty", "name");
            Contract.EndContractBlock();

            return Upload(helper, name, ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));
        }

        /// <summary>
        /// Returns a file input element by using the specified HTML helper, the name of the form field, and the HTML attributes.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An input element whose type attribute is set to "file".</returns>
        public static HtmlString Upload(this HtmlHelper helper, string name, IDictionary<string, object> htmlAttributes)
        {
            if (helper == null)
                throw new ArgumentNullException("helper");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Target cannot be null or empty", "name");
            Contract.EndContractBlock();

            var tagBuilder = BuildTagBuilder(name, htmlAttributes);
            tagBuilder.MergeAttributes<string, object>(helper.GetUnobtrusiveValidationAttributes(name));

            return new HtmlString(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// Returns a file input element for each property in the object that is represented by the specified expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model</typeparam>
        /// <typeparam name="TProperty">The type of the property</typeparam>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <returns>An HTML input element whose type attribute is set to "file" for each property in the object that is represented by the expression.</returns>
        public static HtmlString UploadFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            if (helper == null)
                throw new ArgumentNullException("helper");
            if (expression == null)
                throw new ArgumentNullException("expression");
            Contract.EndContractBlock();

            return UploadFor(helper, expression, null);
        }

        /// <summary>
        /// Returns a file input element by using the specified HTML helper, the name of the form field, and the HTML attributes.
        /// </summary>
        /// <typeparam name="TModel">The type of the model</typeparam>
        /// <typeparam name="TProperty">The type of the property</typeparam>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An input element whose type attribute is set to "file".</returns>
        public static HtmlString UploadFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            if (helper == null)
                throw new ArgumentNullException("helper");
            if (expression == null)
                throw new ArgumentNullException("expression");
            Contract.EndContractBlock();

            return UploadFor(helper, expression, ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));
        }

        /// <summary>
        /// Returns a file input element by using the specified HTML helper, the name of the form field, and the HTML attributes.
        /// </summary>
        /// <typeparam name="TModel">The type of the model</typeparam>
        /// <typeparam name="TProperty">The type of the property</typeparam>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An input element whose type attribute is set to "file".</returns>
        public static HtmlString UploadFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            if (helper == null)
                throw new ArgumentNullException("helper");
            if (expression == null)
                throw new ArgumentNullException("expression");
            Contract.EndContractBlock();

            string name = helper.NameFor(expression).ToString();
            var tagBuilder = BuildTagBuilder(name, htmlAttributes);            
            var metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, helper.ViewData);
            tagBuilder.MergeAttributes<string, object>(helper.GetUnobtrusiveValidationAttributes(name, metadata));

            return new HtmlString(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }

        private static TagBuilder BuildTagBuilder(string name, IDictionary<string, object> htmlAttributes)
        {
            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttribute("type", "file");
            tagBuilder.MergeAttribute("name", name);
            tagBuilder.MergeAttribute("id", HtmlHelper.GenerateIdFromName(name));
            tagBuilder.MergeAttributes(htmlAttributes);
            return tagBuilder;
        }
    }
}
