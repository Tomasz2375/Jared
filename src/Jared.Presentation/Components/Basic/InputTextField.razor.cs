using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace Jared.Presentation.Components.Basic
{
    public partial class InputTextField
    {
        [Parameter]
        public Expression<Func<string>> ValidationFor { get; set; } = default!;
        [Parameter]
        public string? Id { get; set; }
        [Parameter]
        public string? Label { get; set; }

        protected override bool TryParseValueFromString(string? value, out string result, out string validationErrorMessage)
        {
            result = value!;
            validationErrorMessage = null!;
            return true;
        }
    }
}
