﻿module SignalGeneratorTests

open NUnit.Framework
open SignalGenerator

[<TestFixture>]
type ``When generating 2 seconds at 440Hz`` () =
  [<Test>]
  member this.``there should be 88200 samples`` () =
    let samples = generateSamples 2000. 440.
    Assert.AreEqual(8820, Seq.length samples)

  [<Test>]
  member this.``all samples should be in range`` ()=
    let sixteenBitSampleLimit = 32767s
    let samples = generateSamples 2000. 440.
    samples |> Seq.iter (fun s -> Assert.IsTrue(s > (-1s * sixteenBitSampleLimit) && s < sixteenBitSampleLimit))
