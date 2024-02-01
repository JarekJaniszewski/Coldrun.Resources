using AutoFixture.AutoMoq;
using AutoFixture.NUnit3;
using AutoFixture;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Resources.Application.UnitTests.Common
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {

        public AutoMoqDataAttribute() :
            base(() => SetupFixture())
        {
        }

        private static IFixture SetupFixture()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());
            return fixture;
        }
    }
}
