#load "../packages/FSharp.Charting/FSharp.Charting.fsx"
open FSharp.Charting

let generateSamples milliseconds freqency = 
    let sampleRate = 44100.
    let sixteenBitSampleLimit = 32767.
    let volume = 0.8
    let toAmplitude x = 
        x
        |> (*) (2. * System.Math.PI * freqency / sampleRate)
        |> sin
        |> (*) sixteenBitSampleLimit
        |> (*) volume
        |> int16 //convert to int 16
    let numofSamples = milliseconds / 1000. * sampleRate
    let requiredSamples = seq { 1.0..numofSamples } // seq is a sequences comparable to a IEnumerable<T>
    
    Seq.map toAmplitude requiredSamples 
    
// generate 1 second of audio at 440 hertz

// let points = generateSamples 1000. 440.
// points |> Chart.Line