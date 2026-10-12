using System.Runtime.InteropServices;
using Jaro.MidiQwertz.Services;

//MacKeyListenerService.Start();
var midiConnectionService = new MidiConnectionService();
var inputDevice = midiConnectionService.ConnectStandardDeviceAsync();

var midiEventService = new MidiEventService(inputDevice);
var midiEventToPianoStrokeTranslationService = new MidiEventToPianoStrokeTranslationService();
var midiToKeyTranslationService = new MidiToKeyTranslationService();

await foreach (var ev in midiEventService.GetGroupedNoteEventsAsync())
{
    if (ev.Length == 0) continue;
    var pianoStroke = midiEventToPianoStrokeTranslationService.GetPianoStrokeFromNoteEventArray(ev);
    var key = midiToKeyTranslationService.Translate(pianoStroke);
    if (key is not null)
        MacKeySendService.SendKey(key.Value.Key, key.Value.Modifiers);
}
