using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Microsoft.eShopWeb.FunctionalTests.EndToEnd.Pages;
using System;

namespace Microsoft.eShopWeb.FunctionalTests.EndToEnd.Step_2.Assertions
{
    public abstract class PageAssertions<TPage, TParentAssertion> : ReferenceTypeAssertions<TPage, PageAssertions<TPage, TParentAssertion>>
       where TPage : Page, new()
       where TParentAssertion : PageAssertions<TPage, TParentAssertion>
    {
        protected override string Identifier => typeof(TPage).Name;

        public PageAssertions(TPage page)
        {
            Subject = page;
        }

        public AndConstraint<TParentAssertion> HaveUrlEndingWith(string expectedUrl)
        {
            var actualUrl = Subject.Url;
            Execute
               .Assertion
               .ForCondition(actualUrl != null && actualUrl.EndsWith(expectedUrl, StringComparison.InvariantCultureIgnoreCase))
               .FailWith($"Expected page's url to end with {expectedUrl}, but was {actualUrl}");

            return new AndConstraint<TParentAssertion>((TParentAssertion)this);

        }

        public AndConstraint<TParentAssertion> Be<TExpectedPage>(string withExpectedTitle)
            where TExpectedPage : Page, new()
        {
            var actualPageTitle = Subject.Title;
            Execute
               .Assertion
               .ForCondition(typeof(TExpectedPage) == typeof(TPage) && string.Equals(actualPageTitle, withExpectedTitle, StringComparison.InvariantCultureIgnoreCase))
               .FailWith($"Expected page to be {typeof(TExpectedPage).Name} with {withExpectedTitle}, " +
                         $"but was {typeof(TPage).Name} with {actualPageTitle}");

            return new AndConstraint<TParentAssertion>((TParentAssertion)this);

        }

    }
}
