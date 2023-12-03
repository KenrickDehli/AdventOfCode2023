using System.Text;

namespace AoCDayOne;

public static class DayThree
{
    /*
     * 1. Declare what is a symbol and what is not. Ignore dots
     * 2. For each line, parse the number and note the length
     * 3. Check if there is a symbol on the left, right, top, bottom and diagonally
     * 4. If so, then add the number to the sum
     */
    public static int GetSumOfPartNumbers()
    {
        var sum = 0;
        var stringList = Inputs.InputDayThree.Split('\n').ToList();
        var rowWidth = stringList[0].Length;
        for (var i = 0; i < stringList.Count; i++)
        {
            var j = 0;
            while (j < rowWidth)
                if (char.IsNumber(stringList[i][j]))
                {
                    var lengthOfNumber = 0;
                    var numberEnds = false;
                    var sb = new StringBuilder();
                    var k = j;
                    while (char.IsNumber(stringList[i][k]))
                    {
                        sb.Append(stringList[i][k]);
                        lengthOfNumber++;
                        k++;
                        if (k == stringList[i].Length) break;
                    }

                    var numberStartIndex = j;
                    var numberEndIndex = j + lengthOfNumber;
                    var number = int.Parse(stringList[i].Substring(j, lengthOfNumber));
                    Console.Out.WriteLine(number);
                    /*
                     * Now check if number is surronded by symbols
                     * Edge cases
                     * - first and last line
                     * - beginning and end of the line
                     */
                    var indexLeft = numberStartIndex - 1;
                    var indexRight = numberEndIndex;

                    if (!IsEndOrBeginningOfLine(numberStartIndex, rowWidth))
                    {
                        var charLeft = stringList[i][indexLeft];
                        var charRight = stringList[i][indexRight];
                        if (stringList[i][indexLeft] != '.' || stringList[i][indexRight] != '.')
                        {
                            var currentLine = stringList[i];
                            sum += number;
                            j += lengthOfNumber;
                            continue;
                        }
                    }


                    //iterate with lengthOfNumber over the line above
                    var indexAbove = (i - 1, numberStartIndex);
                    // same for below
                    var indexBelow = (i + 1, numberStartIndex);


                    if (!IsFirstOrLastLine(i, stringList.Count))
                    {
                        for (int l = numberStartIndex; l < numberEndIndex; l++)
                        {
                            if (stringList[indexAbove.Item1][l] != '.' || stringList[indexBelow.Item1][l] != '.')
                            {
                                sum += number;
                                break;
                            }
                        }
                    }

                    if (i == 0)
                    {
                        for (int l = numberStartIndex; l < numberEndIndex; l++)
                        {
                            if (stringList[indexBelow.Item1][l] != '.')
                            {
                                sum += number;
                                break;
                            }
                        }
                    }

                    if (i == stringList.Count)
                    {                        
                        for (int l = numberStartIndex; l < numberEndIndex; l++)
                        {
                             if (stringList[indexAbove.Item1][l] != '.')
                             {
                                 sum += number;
                                 break;
                             }
                        }
                    }

                    // var indexDiagonalLeftDown = (i + 1, numberStartIndex - 1);
                    // var indexDiagonalLeftUp = (i - 1, numberStartIndex - 1);
                    // var indexDiagonalRightDown = (i + 1, numberEndIndex + 1);
                    // var indexDiagonalRightUp = (i - 1, numberEndIndex + 1);
                    j += lengthOfNumber;
                }
                else
                {
                    j++;
                }
        }

        return sum;
    }

    private static bool IsFirstOrLastLine(int lineIndex, int lengthOfInput)
    {
        if (lineIndex == 0) return true;

        if (lineIndex == lengthOfInput) return true;
        return false;
    }

    private static bool IsEndOrBeginningOfLine(int currentIndex, int rowWidth)
    {
        if (currentIndex == 0) return true;

        if (currentIndex == rowWidth) return true;
        return false;
    }
}