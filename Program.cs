using Google.API.Search;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Globalization;
using System.Speech.Recognition;
using System.Speech.Recognition.SrgsGrammar;
using System.Speech.Synthesis;
using System.Text;

namespace SEB
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] text = "en-CA|Seb is ready.|Say a command:| .,:;!?|on|of|done|help|Openning the user manual.|find|information|Searching the web for |Sorry, didn't get that.|An error occured: |Exiting|Found {0} results.|Page |Result {0} is {1} from {2}.|next|previous|result|Invalid result number|Result |open|Openning page in web browser...|snippet".Split('|');
            SpeechSynthesizer so = new SpeechSynthesizer();
            SpeechRecognitionEngine si = new SpeechRecognitionEngine(new CultureInfo(text[0]));
            so.SetOutputToDefaultAudioDevice();
            si.SetInputToDefaultAudioDevice();
            si.LoadGrammar(new DictationGrammar());
            so.Speak(text[1]);
        _:
            try
            {
                while (true)
                {
                    so.Speak(text[2]);
                    RecognitionResult r = si.Recognize();
                    string s = r.Text;
                    List<string> words = new List<string>(s.ToLower().Split(text[3].ToCharArray()));
                    words.RemoveAll((string _) => _ == "");
                    string query = "";
                    if (words.Contains(text[6]))
                        goto done;
                    if (words[2] == text[4] | words[2] == text[5])
                        words.RemoveAt(2);
                    for (int i = 2; i < words.Count; ++i)
                        query += " " + words[i];
                    query = query.Remove(0, 1);
                    if (words[1] == text[7])
                    {
                        so.Speak(text[8]);
                        Process.Start(Environment.GetEnvironmentVariable("programdata") + @"\SEB\Manual.pdf");
                    }
                    else if (words[1] == text[10])
                    {
                        so.Speak(text[11] + query);
                        web(query, so, si, text);
                    }
                    else
                        so.Speak(text[12]);
                }
            }
            catch (Exception e)
            {
                so.Speak(text[13] + e.Message);
                goto _;
            }
        done:
            so.Speak(text[14]);
        }
        static void web(string query, SpeechSynthesizer so, SpeechRecognitionEngine si, string[] text)
        {
            GwebSearchClient client = new GwebSearchClient("http://seb.appspot.com/");
            List<IWebResult> result = new List<IWebResult>(client.Search(query, 50));
            so.Speak(string.Format(text[15], result.Count));
            byte pn = 0, rn = 0;
            while (true)
            {
                so.Speak(text[16] + (pn + 1));
                for (byte i = 0; i < 10; ++i)
                    so.Speak(string.Format(text[17], i + 1, result[pn * 10 + i].Title, new Uri(result[pn * 10 + i].Url).Authority));
            pi:
                List<string> s = new List<string>(si.Recognize().Text.ToLower().Replace("one", "1").Replace("two", "2").Replace("three", "3").Replace("four", "4").Replace("five", "5").Replace("six", "6").Replace("seven", "7").Replace("eight", "8").Replace("nine", "9").Replace("ten", "10").Split(text[3].ToCharArray()));
#if DEBUG
            foreach (string x in s)
                Console.WriteLine(x);
#endif
                s.RemoveAll((string x) => x == "");
                if (s.Contains(text[18]))
                {
                    if (pn < (byte)Math.Ceiling(result.Count / 10.0))
                        ++pn;
                }
                else if (s.Contains(text[19]))
                {
                    if (pn > 0)
                        --pn;
                }
                else if (s.Contains(text[6]))
                    break;
                else if (s.Contains(text[20]))
                {
                    try
                    {
                        if (!byte.TryParse(s[s.IndexOf(text[20]) + 1], out rn) | rn > 10 | rn < 1)
                        {
                            so.Speak(text[21]);
                            goto pi;
                        }
                        --rn;
                    }
                    catch
                    {
                        so.Speak(text[12]);
                        goto pi;
                    }
                    while (true)
                    {
                        so.Speak(text[22] + (rn + 1));
                        List<string> _ = new List<string>(si.Recognize().Text.ToLower().Split(text[3].ToCharArray()));
                        _.RemoveAll((string x) => x == "");
                        if (_.Contains(text[23]))
                        {
                            so.Speak(text[24]);
                            Process.Start(result[pn * 10 + rn].Url);
                            break;
                        }
                        else if (_.Contains(text[25]))
                            so.Speak(result[pn * 10 + rn].Content);
                        else if (_.Contains(text[6]))
                            break;
                    }
                }
                else
                {
                    so.Speak("Sorry, didn't get that.");
                    goto pi;
                }
            }
        }
    }
}