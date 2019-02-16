using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class AssignCameraTV : MonoBehaviour
{
    void Start()
    {
        Debug.Log(Application.dataPath);
        System.Console.WriteLine(Application.dataPath);

        //Load Text File
        string line;
        StreamReader theReader = new StreamReader(Application.dataPath, Encoding.Default);

        using (theReader)
        {
            // While there's lines left in the text file, do this:
            do
            {
                line = theReader.ReadLine();

                if (line != null)
                {
                    
                }
            }
            while (line != null);
            theReader.Close();
        }
    }
}
