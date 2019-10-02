using UnityEngine;
// use the following whenever you want to work with files on your operating system- used when creating and opening the save file
using System.IO;
// the following allows us to access the binary formatter, allowing the saving to happen
using System.Runtime.Serialization.Formatters.Binary;

public class s_SaveSystem
{
    public static void SaveGame(s_Player playerInfo)
    {
        // create a binary formatter to use to save the data
        BinaryFormatter formatter = new BinaryFormatter();

        // gets a path to a data directory on the OS that isn't going to suddenly change- this way it knows where to save the data
        // the string appended makes it's own subfile, you can make any file type you want
        string path = Application.persistentDataPath + "/playerData.trumbus";

        // create a new file. everytime you create a new file you work with a file stream. 
            // a FileStream is a stream of data contained in a file. we can use a particular FileStream to read and write from a file
        FileStream stream = new FileStream(path, FileMode.Create);

        // since we made a constructor for the script_PlayerData, all we have to do is pass in the specific player that the SaveGame method takes in and the script_PlayerData class just sets itself up with the constructor method!
        s_PlayerData data = new s_PlayerData(playerInfo);

        // now that the data is formatted how it should be, we are ready to insert this into our file
        // we give it WHERE to save and WHAT to save
            // Serialize = write data to the file in binary
        formatter.Serialize(stream, data);

        // ALWAYS CLOSE THE STREAM. OTHERWISE YOU'LL GET WEIRD LOOKING ERRORS!! 
        stream.Close();
    }

    public static s_PlayerData LoadGame()
    {
        // get a path of where to look for a save
        string path = Path.Combine(Application.persistentDataPath, "playerData.trumbus");

        // check if the file exists in this path
        if (File.Exists(path))
        {
            // if it exists:

            // open up/set up the binary formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // open up file stream of already exisiting save file
            FileStream stream = new FileStream(path, FileMode.Open);

            // read from the stream
                // Deserialize = change from binary to the chosen readible format
            // store this data into a new variable
                // 'as script_PlayerData' = tell it what type of data it is working with by casting it by formating it 'as script_PlayerData'. So now the info is casted into a script_PlayerData type
                // script_PlayerData is like a package where we will take the goodies from
            s_PlayerData data = formatter.Deserialize(stream) as s_PlayerData;

            // ALWAYS CLOSE THE STREAM. OTHERWISE YOU'LL GET WEIRD LOOKING ERRORS!! 
            stream.Close();

            // make sure to return the data to load!
            return data;
        }

        // if a path was not found
        else
        {
            // !!!In the event where the LoadGame() function is called on a new game with 0 progress/saved data, 
            // we mimic the actions of code that would have normally occured after the LoadGame() function was called in it's area's code block
            // IF NOT DONE CORRECTLY THE GAME WILL HAVE MANY ERRORS AND JUST COMPLETELY BREAK!!!

            // return null so the GameManager's Load function knows to do nothing
            return null;
        }
    }
}
