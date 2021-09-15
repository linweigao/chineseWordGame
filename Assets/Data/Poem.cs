using System;
using System.Collections.Generic;

public enum Tag
{
    月,
    目,
    冰,
    家
}

public class Sentence
{
    public Sentence()
    {
        this.Tags = new List<Tag>();
    }

    public string Content { get; set; }
    public List<Tag> Tags { get; set; }
}

public class Poem
{
    public Poem()
    {
        this.Sentences = new List<Sentence>();
    }

    public string Title { get; set; }
    public string Author { get; set; }

    public List<Sentence> Sentences { get; set; }
}

public class PoemMatch
{
    public Poem Poem { get; set; }
    public List<Sentence> Sentences { get; set; }
}

public class PoemList : List<Poem>
{
    private static PoemList instance = new PoemList();
    public static PoemList Instance
    {
        get { return instance; }
    }

    public PoemList()
    {
        var poem = new Poem
        {
            Title = "静夜思",
            Author = "李白",
            Sentences =
            {
                new Sentence { Content="床前明月光，", Tags = {Tag.月 } },
                new Sentence { Content="疑是地上霜。", Tags = {Tag.冰}},
                new Sentence{Content="举头望明月，", Tags = {Tag.月 } },
                new Sentence{Content="低头思故乡。", Tags = {Tag.家}}
            }
        };

        this.Add(poem);
    }

    public PoemMatch Match(string input)
    {
        foreach (var poem in this)
        {
            var poemMatch = poem.Match(input);
            if (poemMatch != null)
            {
                return poemMatch;
            }
        }

        return null;
    }
}

public static class PoemExtensions
{
    public const string ChineseSymbol = "，。！？";

    public static bool Match(this Sentence sentence, string input)
    {
        int count = 0;
        int match = 0;
        foreach (var cs in sentence.Content)
        {
            if (ChineseSymbol.Contains(cs.ToString()))
            {
                continue;
            }

            count++;

            if (input.Contains(cs.ToString()))
            {
                match++;
            }
        }

        return (match * 100 / count >= 70);
    }

    public static PoemMatch Match(this Poem poem, string input)
    {
        var sentences = poem.Sentences.FindAll(s => s.Match(input));
        if (sentences.Count > 0)
        {
            return new PoemMatch { Poem = poem, Sentences = sentences };
        }

        return null;
    }
}
