namespace VertigoGamesAssignment.Models;

internal class Property
{
    public string Name { get; private set; }
    public string Value { get; set; }
    public string[] PossibleValues { get; private set; }

    public Property(string name, string value, IEnumerable<string> possibleValues)
    {
        Name = name;
        Value = value;
        PossibleValues = possibleValues.ToArray();
    }
}