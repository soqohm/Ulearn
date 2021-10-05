using System;
using System.Text;

namespace hashes
{
    public class GhostsTask :
        IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>,
        IMagic
    {
        Document document;
        Vector vector;
        Segment segment;
        Cat cat;
        Robot robot;
        byte[] docContent = { 1, 1 };

        public GhostsTask()
        {
            cat = new Cat("ryzhij", "kot", new DateTime());
            robot = new Robot("dzharvis");
            vector = new Vector(1, 1);
            segment = new Segment(vector, vector);
            document = new Document("ghoststask", Encoding.Unicode, docContent);
        }

        public void DoMagic()
		{
            cat.Rename("рыжий");
            Robot.BatteryCapacity++;
            vector.Add(new Vector(7, 7));
            docContent[0] = 7;
        }

        Document IFactory<Document>.Create() { return document; }

        Vector IFactory<Vector>.Create() { return vector; }

        Segment IFactory<Segment>.Create() { return segment; }

        Cat IFactory<Cat>.Create() { return cat; }

        Robot IFactory<Robot>.Create() { return robot; }
    }
}