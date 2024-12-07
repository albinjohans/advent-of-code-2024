using System.Diagnostics.CodeAnalysis;

bool isPossibleExpression(long wantedResult, long currentValue, List<long> remainingExpression)
{
    if (remainingExpression.Count == 0)
    {
        return currentValue == wantedResult;
    }
    else if (currentValue > wantedResult)
    {
        return false;
    }
    else
    {
        long nextValue = remainingExpression[0];

        bool sumExpression = isPossibleExpression(wantedResult, currentValue + nextValue, remainingExpression[1..]);
        bool multiplicationExpression = isPossibleExpression(wantedResult, currentValue * nextValue, remainingExpression[1..]);
        bool concatenationExpression = isPossibleExpression(wantedResult, currentValue * (long)(Math.Pow(10, Math.Floor(Math.Log10(nextValue)) + 1)) + nextValue, remainingExpression[1..]);
        return multiplicationExpression || sumExpression || concatenationExpression;
    }
}

var lines = File.ReadLines("input.txt");
long answer = 0;

foreach (var line in lines) 
{
    string[] subStrings = line.Split(" ");
    List<string> lineList = [.. subStrings];
    lineList[0] = lineList[0].Replace(":", "");
    List<long> expressionList = lineList.ConvertAll(long.Parse);
    long wantedResult = expressionList[0];
    expressionList.RemoveAt(0);

    long startingValue = expressionList[0];
    expressionList.RemoveAt(0);

    if (isPossibleExpression(wantedResult, startingValue, expressionList)) { answer += wantedResult; };
}

Console.WriteLine(answer);
