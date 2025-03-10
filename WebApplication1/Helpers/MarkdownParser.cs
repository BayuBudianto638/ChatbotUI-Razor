using System;
using System.Text.RegularExpressions;
using System.Text;

namespace WebApplication1.Helpers;

public class MarkdownParser
{
    public static string ParseMarkdown(string markdown)
    {
        if (string.IsNullOrWhiteSpace(markdown))
            return string.Empty;

        markdown = markdown.Replace("<", "&lt;").Replace(">", "&gt;");

        markdown = Regex.Replace(markdown, @"^###### (.*)", "<h6>$1</h6>", RegexOptions.Multiline);
        markdown = Regex.Replace(markdown, @"^##### (.*)", "<h5>$1</h5>", RegexOptions.Multiline);
        markdown = Regex.Replace(markdown, @"^#### (.*)", "<h4>$1</h4>", RegexOptions.Multiline);
        markdown = Regex.Replace(markdown, @"^### (.*)", "<h3>$1</h3>", RegexOptions.Multiline);
        markdown = Regex.Replace(markdown, @"^## (.*)", "<h2>$1</h2>", RegexOptions.Multiline);
        markdown = Regex.Replace(markdown, @"^# (.*)", "<h1>$1</h1>", RegexOptions.Multiline);

        markdown = Regex.Replace(markdown, @"\*\*\*(.*?)\*\*\*", "<b><i>$1</i></b>");
        markdown = Regex.Replace(markdown, @"\*\*(.*?)\*\*", "<b>$1</b>");
        markdown = Regex.Replace(markdown, @"\*(.*?)\*", "<i>$1</i>");
        markdown = Regex.Replace(markdown, @"~~(.*?)~~", "<del>$1</del>");

        markdown = Regex.Replace(markdown, @"^> (.*)", "<blockquote>$1</blockquote>", RegexOptions.Multiline);

        markdown = Regex.Replace(markdown, @"^\* (.*)", "<ul><li>$1</li></ul>", RegexOptions.Multiline);
        markdown = Regex.Replace(markdown, @"^- (.*)", "<ul><li>$1</li></ul>", RegexOptions.Multiline);
        markdown = Regex.Replace(markdown, @"^(\d+)\. (.*)", "<ol><li>$2</li></ol>", RegexOptions.Multiline);

        markdown = Regex.Replace(markdown, @"\[(.*?)\]\((.*?)\)", "<a href=\"$2\">$1</a>");

        markdown = Regex.Replace(markdown, @"!\[(.*?)\]\((.*?)\)", "<img src=\"$2\" alt=\"$1\">");

        markdown = Regex.Replace(markdown, @"```(.*?)```", "<pre><code>$1</code></pre>", RegexOptions.Singleline);
        markdown = Regex.Replace(markdown, @"`(.*?)`", "<code>$1</code>");

        markdown = Regex.Replace(markdown, @"^(\*\*\*|---|___)\s*$", "<hr>", RegexOptions.Multiline);

        markdown = Regex.Replace(markdown, @"</ul>\s*<ul>", "", RegexOptions.Multiline);
        markdown = Regex.Replace(markdown, @"</ol>\s*<ol>", "", RegexOptions.Multiline);

        return markdown.Trim();
    }
}