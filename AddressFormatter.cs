using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace MWM_Assignment_New
{
    internal sealed class AddressParts
    {
        internal string Line1 { get; set; }
        internal string Line2 { get; set; }
        internal string City { get; set; }
        internal string State { get; set; }
        internal string Postcode { get; set; }
        internal string Country { get; set; }
    }

    internal static class AddressFormatter
    {
        private static readonly string[] Labels = { "Line1", "Line2", "City", "State", "Postcode", "Country" };

        internal static string Combine(AddressParts parts)
        {
            List<string> lines = new List<string>
            {
                "Line1: " + Clean(parts.Line1),
                "Line2: " + Clean(parts.Line2),
                "City: " + Clean(parts.City),
                "State: " + Clean(parts.State),
                "Postcode: " + Clean(parts.Postcode),
                "Country: " + Clean(parts.Country)
            };

            return string.Join(Environment.NewLine, lines);
        }

        internal static AddressParts Split(string combinedAddress)
        {
            AddressParts parts = new AddressParts();

            if (string.IsNullOrWhiteSpace(combinedAddress))
            {
                parts.Country = "Malaysia";
                return parts;
            }

            string[] lines = combinedAddress
                .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Trim())
                .ToArray();

            bool hasLabels = lines.Any(line => Labels.Any(label => line.StartsWith(label + ":", StringComparison.OrdinalIgnoreCase)));
            if (hasLabels)
            {
                foreach (string line in lines)
                {
                    int separator = line.IndexOf(':');
                    if (separator < 0)
                    {
                        continue;
                    }

                    string label = line.Substring(0, separator).Trim();
                    string value = line.Substring(separator + 1).Trim();
                    SetValue(parts, label, value);
                }

                if (string.IsNullOrWhiteSpace(parts.Country))
                {
                    parts.Country = "Malaysia";
                }

                return parts;
            }

            string[] guessed = combinedAddress
                .Split(new[] { ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(part => part.Trim())
                .Where(part => part.Length > 0)
                .ToArray();

            if (guessed.Length > 0) parts.Line1 = guessed[0];
            if (guessed.Length > 1) parts.City = guessed[1];
            if (guessed.Length > 2) parts.Postcode = guessed[2];
            if (guessed.Length > 3) parts.State = guessed[3];
            parts.Country = guessed.Length > 4 ? guessed[4] : "Malaysia";
            return parts;
        }

        internal static void AddRequiredValidator(RequiredFieldValidator validator, TextBox textBox, string errorMessage)
        {
            validator.ControlToValidate = textBox.ID;
            validator.ErrorMessage = errorMessage;
            validator.Text = "*";
            validator.CssClass = "text-danger small";
            validator.Display = ValidatorDisplay.Dynamic;
        }

        private static string Clean(string value)
        {
            return (value ?? string.Empty).Trim();
        }

        private static void SetValue(AddressParts parts, string label, string value)
        {
            switch (label.ToLowerInvariant())
            {
                case "line1":
                    parts.Line1 = value;
                    break;
                case "line2":
                    parts.Line2 = value;
                    break;
                case "city":
                    parts.City = value;
                    break;
                case "state":
                    parts.State = value;
                    break;
                case "postcode":
                    parts.Postcode = value;
                    break;
                case "country":
                    parts.Country = value;
                    break;
            }
        }
    }
}
