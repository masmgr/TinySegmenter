# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

TinySegmenter.NET is a C# implementation of a super compact Japanese tokenizer. It's a port of the original JavaScript TinySegmenter library that uses machine learning-based character-level morphological analysis to segment Japanese text into words without requiring a dictionary.

## Core Architecture

### Main Components

- **[TinySegmenter.cs](TinySegmenter/TinySegmenter.cs)**: Contains the single public class `TinySegmenter` with one static method `Segment(string input)` that tokenizes Japanese text.
- **[TinySegmenterTests.cs](TinySegmenterTests/TinySegmenterTests.cs)**: MSTest-based unit tests verifying segmentation accuracy.

### How It Works

The `Segment` method implements a character-based, machine learning approach to Japanese word segmentation:

1. **Character Classification**: Each character is classified into one of 6 types (M=number, H=Kanji, I=Hiragana, K=Katakana, A=Latin, N=Digit, O=Other) using compiled regex patterns.

2. **Scoring System**: The algorithm maintains a sliding window of context (±3 characters) and uses pre-trained weight tables to calculate a segmentation score at each position. A positive score indicates a word boundary.

3. **Weight Tables**: The class contains 40+ readonly dictionaries (BC1-BC3, BP1-BP2, BQ1-BQ4, BW1-BW3, TC1-TC4, TQ1-TQ4, TW1-TW4, UC1-UC6, UP1-UP3, UQ1-UQ3, UW1-UW6) that store coefficients for different character/context combinations. These weights were derived from the original training algorithm.

4. **Output**: Segments are accumulated in a StringBuilder and yielded as an IEnumerable<string>.

## Development Commands

### Build
```bash
dotnet build
```
Build in Debug configuration (default).

```bash
dotnet build --configuration Release
```
Build optimized Release configuration.

### Run Tests
```bash
dotnet test
```
Run all tests using the default MSTest framework.

```bash
dotnet test --verbosity normal
```
Run tests with standard verbosity.

```bash
dotnet test TinySegmenterTests/TinySegmenterTests.csproj
```
Run tests from the specific test project.

### Restore Dependencies
```bash
dotnet restore
```
Restore NuGet packages as defined in the project files.

## Project Structure

- **TinySegmenter/**: Main library project (.NET 8.0, ImplicitUsings enabled, nullable enabled)
- **TinySegmenterTests/**: MSTest unit test project with dependencies on MSTest v3.1.1
- **.github/workflows/ci.yml**: CI pipeline that builds and tests on every push/PR to main

## Key Implementation Details

### Character Type Classification
Character types are determined by regex patterns defined in `chartype_` list. The regex patterns are compiled once for performance.

### Scoring Algorithm
The `CalcScore` method combines scores from multiple lookup tables based on:
- Previous segmentation state (p1, p2, p3 - unigram, bigram, trigram of previous boundaries)
- 6-character context window (w1-w6 around current position)
- Character types of the context window (c1-c6)

The algorithm accumulates scores from single, pair, and triple character/type combinations.

### Dependencies
- **Microsoft.NET.Sdk**: Standard .NET SDK
- **MSTest.TestFramework/TestAdapter**: Unit testing framework
- **coverlet.collector**: Code coverage reporting

## Coding Conventions

- Use `readonly` for immutable data structures (all weight dictionaries are `IReadOnlyDictionary<string, int>`)
- Use `IReadOnlyList` for immutable collections
- Private helper methods use snake_case naming (e.g., `CType`, `Ts`, `CalcScore`)
- Public API uses PascalCase (e.g., `Segment`)
- String concatenation is preferred for key lookups over StringBuilder for small strings
