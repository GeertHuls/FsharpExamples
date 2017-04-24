module Assembler

(*
    This module coordinates the score parser, signal generator 
    and wave packer to convert a score into an audio file.
*)

open System.IO
open Parsing
open SignalGenerator
open WavePacker

let tokenToSound token =
    generateSamples (durationFromToken token) (frequency token.sound)
    |> Array.ofSeq

let assemble tokens =
    List.map tokenToSound tokens |> Array.concat

let assembleToPackedStream (score:string) = 
    match parse score with
        | Choice1Of2 errorMsg -> Choice2Of2 errorMsg
        | Choice2Of2 tokens ->
          assemble tokens
          |> Array.ofSeq
          |> pack
          |> Choice1Of2
