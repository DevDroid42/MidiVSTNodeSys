using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeSysCore;

namespace VstNetMidiPlugin
{

    //stores array of all notes
    class MidiTable
    {
        static UdpNetworking networking;
        byte[] notes = new byte[144];

        public MidiTable()
        {
            if(networking == null)
            {
                networking = new UdpNetworking("127.0.0.1", 11000);
            }
        }
        public void ProcessNote(byte[] noteData)
        {
            //networking.SendDebug("Debug: ", " 0:" + noteData[0]+ " 1:" + noteData[1] + " 2:" + noteData[2]);
            
            //Notedata[0] 144 is note down 128 is note up but note up has velocity of zero                                               
            if(noteData[0] == 128)
            {
                notes[noteData[1]] = 0;
            }
            else
            {
                notes[noteData[1]] = noteData[2];
            }
        }

        public void PublishTable(string ID)
        {
            networking.SendByteArray(ID, notes);
        }
    }    
}
