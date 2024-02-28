using Spectre.Console;
using System.Globalization;
using VertigoGamesAssignment.Models;

namespace VertigoGamesAssignment.Console.Menus;
internal static class MiscMenus
{
    public static void Exit()
    {
        bool exit = AnsiConsole.Confirm("[yellow]Are you sure you want to exit?[/]");
        AnsiConsole.Clear();
        if (exit)
        {
            AnsiConsole.MarkupLine("[lime]Until next time![/]");
            AnsiConsole.MarkupLine($"[grey]Press any key to exit[/]");
            AnsiConsole.Console.Input.ReadKey(false);
            Environment.Exit(0);
        }
    }

    public static void ItemCount(ShoppingCartItem item)
    {
        AnsiConsole.Clear();
        while (true)
        {
            AnsiConsole.MarkupLine($"[lime][bold]==========================Select [red]{item.Item.Name}[/] count==========================[/][/]");
            AnsiConsole.MarkupLine($"[blue][bold]<------------------------------------{item.Count}-------------------------------------->[/][/]");
            AnsiConsole.MarkupLine($"[grey]Use left/right arrow to adjust count, Enter to confirm[/]");
            ConsoleKeyInfo? key = AnsiConsole.Console.Input.ReadKey(false);
            AnsiConsole.Clear();
            if (key == null)
            {
                continue;
            }
            if (key.Value.Key == ConsoleKey.LeftArrow && item.Count > 1)
            {
                item.Count -= 1;
            }
            else if (key.Value.Key == ConsoleKey.RightArrow && item.Count < Constants.MaxItemCount)
            {
                item.Count += 1;
            }
            else if (key.Value.Key == ConsoleKey.Enter)
            {
                break;
            }
        }
    }

    public static bool Checkout(ShoppingCart shoppingCart)
    {
        AnsiConsole.MarkupLine($"[lime][bold]==========================Your order:==========================[/][/]");

        Table table = new();
        table.AddColumn("Item");
        table.AddColumn(new TableColumn("Count").Centered());
        table.AddColumn("Misc");
        table.AddColumn(new TableColumn("Price").Centered());
        table.AddColumn(new TableColumn("Subtotal").Centered());

        foreach (ShoppingCartItem item in shoppingCart.Items)
        {
            table.AddRow(
                item.Item.Name,
                item.Count.ToString("0.00", CultureInfo.InvariantCulture),
                item.PropertyString,
                $"EUR {item.Item.Price.ToString("0.00", CultureInfo.InvariantCulture)}",
                $"EUR {(item.Item.Price * item.Count).ToString("0.00", CultureInfo.InvariantCulture)}");
        }

        AnsiConsole.Write(table);

        AnsiConsole.MarkupLine("");
        AnsiConsole.MarkupLine("[lime]Please submit your personal details[/]");

        string firstName = AnsiConsole.Ask<string>("[yellow]Enter your first name:[/]")!;
        string lastName = AnsiConsole.Ask<string>("[yellow]Enter your last name:[/]")!;
        string? company = AnsiConsole.Prompt(new TextPrompt<string>("[yellow](Optional)If it's a business order, please enter company name:[/]").AllowEmpty());
        string address = AnsiConsole.Ask<string>("[yellow]Enter delivery address:[/]")!;

        table = new();

        table.AddColumn("");
        table.AddColumn("");
        table.HideHeaders();

        table.AddRow("First name", firstName);
        table.AddRow("Last name", lastName);
        table.AddRow("Company", string.IsNullOrWhiteSpace(company) ? "(none)" : company);
        table.AddRow("Address", address);

        AnsiConsole.MarkupLine("");
        AnsiConsole.MarkupLine($"[lime][bold]Your details:[/][/]");
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("");

        if (!AnsiConsole.Confirm("[yellow]Please check your personal details. Proceed to checkout?[/]"))
        {
            AnsiConsole.Clear();
            return false;
        }

        SelectionPrompt<string> prompt = new();
        prompt.Title = "[yellow][bold]Select payment method[/][/]";
        const string cash = "Cash";
        const string card = "Card";
        prompt.AddChoices(cash, card);
        string checkoutMethod = AnsiConsole.Prompt(prompt);

        if (checkoutMethod == card)
        {
            string cardNumber = AnsiConsole.Prompt(
                new TextPrompt<string>("[yellow][bold]Enter your credit card number:[/][/]")
                .ValidationErrorMessage("[red]Incorrect credit card number format[/]")
                .Validate(number =>
                {
                    number = number.Replace(" ", "");
                    if (!System.Text.RegularExpressions.Regex.IsMatch(number, @"^\d+$"))
                    {
                        return ValidationResult.Error("[red]Credit card number must contain only digits[/]");
                    }
                    if (number.Length != 16)
                    {
                        return ValidationResult.Error("[red]Credit card number must contain exactly 16 digits[/]");
                    }
                    return ValidationResult.Success();
                }));

            string cvv = AnsiConsole.Prompt(
                new TextPrompt<string>("[yellow][bold]Enter your CVV/CVC code:[/][/]")
                .Secret()
                .ValidationErrorMessage("[red]Incorrect CVV/CVC code format[/]")
                .Validate(CVV =>
                {
                    CVV = CVV.Replace(" ", "");
                    if (!System.Text.RegularExpressions.Regex.IsMatch(CVV, @"^\d+$"))
                    {
                        return ValidationResult.Error("[red]CVV/CVC code must contain only digits[/]");
                    }
                    if (CVV.Length != 3)
                    {
                        return ValidationResult.Error("[red]CVV/CVC code must contain exactly 3 digits[/]");
                    }
                    return ValidationResult.Success();
                }));

            while (true)
            {
                AnsiConsole.Status()
                    .Start("Payment", ctx =>
                    {
                        ctx.Spinner(Spinner.Known.Material);
                        ctx.SpinnerStyle(Style.Parse("yellow"));
                        ctx.Status("[yellow][italic]Connecting to a gateway...[/][/]");
                        Thread.Sleep(2000);
                        ctx.Status("[yellow][italic]Exchanging data...[/][/]");
                        Thread.Sleep(2000);
                        ctx.Status("[yellow][italic]Finishing...[/][/]");
                        Thread.Sleep(2000);
                    });
                if (Random.Shared.NextDouble() < Constants.PaymentSuccessRate)
                {
                    break;
                }
                if (!AnsiConsole.Confirm("[red]Payment failed, retry?[/]"))
                {
                    AnsiConsole.Clear();
                    return false;
                }
            }

            AnsiConsole.Status()
                .Start("Success", ctx =>
                {
                    ctx.SpinnerStyle(Style.Parse("lime"));
                    ctx.Status("[lime][bold]Success![/][/]");
                    Thread.Sleep(2000);
                });
        }

        AnsiConsole.Clear();
        AnsiConsole.MarkupLine(
            $"[lime][bold]Thank you for you purchase, {firstName} {lastName}! " +
            $"Your order will be delivered to you shortly[/][/]\n" +
            $"[grey]Press any key to return to main menu[/]");
        AnsiConsole.Console.Input.ReadKey(false);
        AnsiConsole.Clear();
        return true;
    }
}