# TinySegmenter.NET

A C# port of [TinySegmenter](http://chasen.org/~taku/software/TinySegmenter/), a super compact Japanese tokenizer that requires no dictionary.

## Overview

TinySegmenter.NET is a lightweight Japanese word segmentation library that uses machine learning-based character-level analysis to break Japanese text into tokens. It's ideal for applications that need Japanese text processing without the overhead of large morphological dictionaries.

**Key Features:**
- No dictionary required
- Compact implementation (~1600 lines)
- High accuracy for general Japanese text
- Fast character-based processing
- Simple API

## Installation

Clone the repository and build the project:

```bash
git clone https://github.com/yourusername/TinySegmenter.git
cd TinySegmenter
dotnet build
```

Or build the release version:

```bash
dotnet build --configuration Release
```

## Usage

```csharp
using TinySegmenter;

// Segment Japanese text
var tokens = TinySegmenter.Segment("私の名前は中野です");

// Output: 私 | の | 名前 | は | 中野 | です
Console.WriteLine(string.Join(" | ", tokens));
```

The `Segment` method accepts a string and returns an `IEnumerable<string>` of tokens.

## How It Works

TinySegmenter uses a character-based scoring approach:

1. **Character Classification**: Characters are classified into 6 types (Kanji, Hiragana, Katakana, Latin, Digit, Other)
2. **Context Scoring**: For each position in the text, a score is calculated based on:
   - The previous two segmentation decisions
   - A 6-character context window around the current position
   - Pre-trained weight tables
3. **Segmentation**: When the score exceeds a threshold, a word boundary is inserted

This approach was derived from the original TinySegmenter algorithm and provides good accuracy for typical Japanese text.

## Testing

Run the test suite:

```bash
dotnet test
```

Example test:
```csharp
[TestMethod()]
public void SegmentTest()
{
    var tokens = TinySegmenter.Segment("私の名前は中野です");
    Assert.AreEqual("私 | の | 名前 | は | 中野 | です", string.Join(" | ", tokens));
}
```

## Requirements

- .NET 8.0 or later
- C# 11 or later

## License

TinySegmenter.NET is based on the original [TinySegmenter](http://chasen.org/~taku/software/TinySegmenter/) by Taku Kudo, which is freely distributable under the terms of a new BSD license.

See the source code for the original copyright notice and license terms.

## Original Work

- **TinySegmenter** (JavaScript): © 2008 Taku Kudo
- **TinySegmenter.NET** (C#): © 2010 DOBON! (original .NET port)

## Contributing

Contributions are welcome. Please feel free to submit issues and pull requests.
