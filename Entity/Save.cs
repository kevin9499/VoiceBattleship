using System;

namespace board_game
{
    class Save
    {
        static String path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//BattleShip.xml";

        public void WriteXML(Player player)
        {
            System.Xml.Serialization.XmlSerializer writer =
                           new System.Xml.Serialization.XmlSerializer(typeof(Player));

            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, player);
            file.Close();
        }

        public static void readXML()
        {
            System.Xml.Serialization.XmlSerializer reader =
                            new System.Xml.Serialization.XmlSerializer(typeof(Player));
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            Player player = (Player)reader.Deserialize(file);
            file.Close();
            Console.WriteLine(player);
        }
    }
}
