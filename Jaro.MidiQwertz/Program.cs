using System.Runtime.InteropServices;
using Jaro.MidiQwertz.Services;

//MacKeyListenerService.Start();
var midiConnectionService = new MidiConnectionService();
var inputDevice = midiConnectionService.ConnectStandardDeviceAsync();

var midiEventService = new MidiEventService(inputDevice);
var midiEventToPianoStrokeTranslationService = new MidiEventToPianoStrokeTranslationService();
var midiToKeyTranslationService = new MidiToKeyTranslationService();

await foreach (var ev in midiEventService.AggregateNotesAsync(midiEventService.GetNoteEventsAsync(CancellationToken.None), CancellationToken.None))
{
    if (ev.Length == 0) continue;
    var pianoStroke = midiEventToPianoStrokeTranslationService.GetPianoStrokeFromNoteEventArray(ev);
    Console.WriteLine(pianoStroke.Note);
    continue;
    var key = midiToKeyTranslationService.Translate(pianoStroke);
    if (key is not null)
        MacKeySendService.SendKeyUpDown(key.Value.Key, key.Value.Modifiers);
}
