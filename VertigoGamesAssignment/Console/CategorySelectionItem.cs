﻿namespace VertigoGamesAssignment.Console;

internal class CategorySelectionItem:SelectionItem
{
    public ChoiceSelectionItem[] Choices { get; private set; }
    public CategorySelectionItem(string text, params ChoiceSelectionItem[] choices) : base(text)
    {
        Choices = choices;
    }

    public CategorySelectionItem(string text, IEnumerable<ChoiceSelectionItem> choices) : base(text)
    {
        Choices = choices.ToArray();
    }
}