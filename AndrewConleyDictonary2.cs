namespace Dictonary2;

using System;
using System.Collections.Generic;//for classes to colect/store strings better. array lists.

public class MyDictionary
{
    private List<List<string>> bins;
    private int maxBins;
    private int totalCount;
    //the max bins does not matter, we aint getten that many
    public MyDictionary(int maxBins = 1000)
    {
        this.maxBins = maxBins;
        bins = new List<List<string>>(maxBins);
        for (int i = 0; i < maxBins; i++)
        {
            bins.Add(new List<string>());
        }
        totalCount = 0;
    }

    //hash algerithm
    private int HashFunction(string str)
    {
        int sumAscii = 0;

        //sum ascii
        foreach (char c in str)
        {
            sumAscii += (int)c;
        }

        //avg ascii
        int averageAscii = sumAscii / str.Length;

        //remainder of ascii is what bin
        return averageAscii % maxBins;
    }

    //add word to dict
    public void Add(string str)
    {
        int binIndex = HashFunction(str);
        bins[binIndex].Add(str); //add word to bin
        totalCount++;
    }

    //look for word, returns for me to read for testing purposes
    public bool Contains(string str)
    {
        int binIndex = HashFunction(str);
        return bins[binIndex].Contains(str);
    }

    //when 2 same word found one is trashed because it does not have a purpose being there
    public void Remove(string str)
    {
        int binIndex = HashFunction(str);
        bool removed = bins[binIndex].Remove(str);
        if (removed)
        {
            totalCount--;
        }
    }

    //total number of strings in the dictionary
    public int Count
    {
        get { return totalCount; }
    }

    //find number of strings in one bin for finding algorithm after bin found
    public int BinCount(int binIndex)
    {
        if (binIndex >= maxBins || binIndex < 0)
        {
            throw new ArgumentOutOfRangeException("Bin index is out of range.");
        }

        return bins[binIndex].Count;
    }

    //printing for test usage. I like to read the bens and see what is happening easier. Get rid of this if you want for your usage.
    //print bin then words
    public void PrintBinContents()
    {
        for (int i = 0; i < maxBins; i++)
        {
            Console.Write($"Bin {i}: ");
            foreach (var word in bins[i])
            {
                Console.Write(word + " ");
            }
            Console.WriteLine();
        }
    }


    //find word in dictionary. find bin same as putting word in bin. if more than one word in bin loop check until word found
    public string Find(string word)
    {
        int binIndex = HashFunction(word);
        List<string> bin = bins[binIndex];
        int wordIndex = bin.IndexOf(word);

        if (wordIndex >= 0)
        {
            return $"Word '{word}' found in Bin {binIndex} at position {wordIndex}.";//$ to mix string and int in same thingy
        }
        else
        {
            return $"Word '{word}' not found in the dictionary.";
        }
    }
}

public class Program
{
    public static void Main()
    {
        //test dict
        MyDictionary dict = new MyDictionary(10);//set maxBins to 10 for easy testing, does not really matter for this case, but the total bin number in any case is based on remainder size which is based on the word itself. 

        //words to test with
        List<string> words = new List<string>
        {
            "apple", "banana", "orange", "grape", "kiwi", "melon", "watermelon", "pear", "peach", "plum"
        };

        //add words to dict, its a list rn until all the work done after
        foreach (var word in words)
        {
            dict.Add(word);
        }

        //read amount of words in list for easy usage /for readability on my part, this is for testing, get rid of it if you want to for your usage
        Console.WriteLine($"Total count of strings: {dict.Count}");

        //run the print contents
        dict.PrintBinContents();


        //test find for exsisting & nonexsisting words
        Console.WriteLine(dict.Find("orange")); //word found
        Console.WriteLine(dict.Find("mango"));  //word not found
    }
}
