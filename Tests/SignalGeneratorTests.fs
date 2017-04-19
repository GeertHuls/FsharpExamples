module SignalGeneratorTests

open NUnit.Framework
open SignalGenerator

[<TestFixture>]
type ``When generating 2 seconds at 440Hz`` () =
  [<Test>]
  member this.``there should be 88200 samples`` () =
    let samples = generateSamples 2000. 440.
    Assert.AreEqual(8820, Seq.length samples)
