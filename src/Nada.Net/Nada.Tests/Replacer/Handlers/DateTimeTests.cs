﻿using System.Globalization;
using FluentAssertions;
using Nada.Core.Replacer;
using Nada.Core.Replacer.Handlers;
using NUnit.Framework;

namespace Nada.Core.Tests.Replacer.Handlers;

public class DateTimeTests
{
    [Test]
    public void Replace_Without_Format_Specified()
    {
        var loginDate = DateTime.Now;
        var dict = new Dictionary<string, string>
        {
            { "loginDate", loginDate.ToString("o") }
        };

        // Sample template
        var template = "Last login at {dateTime|loginDate}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateTimeTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Last login at {loginDate.ToString(ci)}");
    }

    [Test]
    public void Replace_With_DateOnly_Format_Specified()
    {
        var loginDate = DateTime.Now;
        var dict = new Dictionary<string, string>
        {
            { "loginDate", loginDate.ToString("o") }
        };

        // Sample template
        var template = "Last login at {dateTime|loginDate|d}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateTimeTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Last login at {loginDate.ToString("d", ci)}");
    }

    [Test]
    public void Replace_With_TimeOnly_Format_Specified()
    {
        var loginDate = DateTime.Now;
        var dict = new Dictionary<string, string>
        {
            { "loginDate", loginDate.ToString("o") }
        };

        // Sample template
        var template = "Last login at {dateTime|loginDate|T}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateTimeTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Last login at {loginDate.ToString("T", ci)}");
    }

    [Test]
    public void Replace_With_TimeOnly2_Format_Specified()
    {
        var loginDate = DateTime.Now;
        var dict = new Dictionary<string, string>
        {
            { "loginDate", loginDate.ToString("o") }
        };

        // Sample template
        var template = "Last login at {dateTime|loginDate|t}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateTimeTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Last login at {loginDate.ToString("t", ci)}");
    }

    [Test]
    public void Replace_With_Custom_Format_Specified()
    {
        var loginDate = DateTime.Now;
        var dict = new Dictionary<string, string>
        {
            { "loginDate", loginDate.ToString("o") }
        };

        // Sample template
        var template = "Last login at {dateTime|loginDate|yyyy-MM-ddThh:mm:ss}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateTimeTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Last login at {loginDate.ToString("yyyy-MM-ddThh:mm:ss", ci)}");
    }

    [Test]
    public void Replace_When_Missing_Value()
    {
        var dict = new Dictionary<string, string>();

        // Sample template
        var template = "Last login at {datetime|loginDate}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateTimeTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }

    [Test]
    public void Replace_With_Null_Value()
    {
        var dict = new Dictionary<string, string>
        {
            { "loginDate", null! }
        };

        // Sample template
        var template = "Last login at {datetime|loginDate}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateTimeTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }

    [Test]
    public void Replace_With_Invalid_Value()
    {
        var dict = new Dictionary<string, string>
        {
            { "loginDate", "zzzz" }
        };

        // Sample template
        var template = "Last login at {datetime|loginDate}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateTimeTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }


    [Test]
    public void Replace_UTC_Date_Time()
    {
        var loginDate = DateTime.UtcNow;
        var dict = new Dictionary<string, string>
        {
            { "loginDate", loginDate.ToString("o") }
        };

        // Sample template
        var template = "Last login at {dateTime|loginDate}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateTimeTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Last login at {loginDate.ToLocalTime().ToString(ci)}");
    }
}