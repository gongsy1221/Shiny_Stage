using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

        string[] data = csvData.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });

            Dialogue dialogue = new Dialogue();
            dialogue.name = row[i];
            List<string> contextList = new List<string>();
            List<string> eventList = new List<string>();
            List<string> skipList = new List<string>();

            do
            {
                contextList.Add(row[2]);
                eventList.Add(row[3]);
                skipList.Add(row[4]);

                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else
                {
                    break;
                }
            } while (row[0].ToString() == "");

            dialogue.contexts = contextList.ToArray();
            dialogue.number = eventList.ToArray();
            dialogue.skipnum = skipList.ToArray();

            dialogueList.Add(dialogue);

            GameObject obj = GameObject.Find("DialogueManager");
            obj.GetComponent<InteractionEvent>().lineY = dialogueList.Count;
        }

        return dialogueList.ToArray();
    }
}
