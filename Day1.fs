module Day1
open System
open Microsoft.FSharp.Reflection

type Direction = North | East | South | West

type Rotation = Left | Right

type Instruction = Rotate of Rotation | Move

type Position = { X: int; Y: int } with
    member this.Distance =
        (abs this.X) + (abs this.Y)
    static member (+) (a, b) =
        { X = a.X + b.X; Y = a.Y + b.Y }
    static member (*) (p, s) =
        { X = p.X * s; Y = p.Y * s }

type State =
    | Result of Position
    | Collection of Set<Position>

let directions = [North;East;South;West]

let rotate rot dir =
    let i = List.findIndex (fun d -> d = dir) directions
    let j = (match rot with
            | Right -> i + 1
            | Left -> i + 3)
    directions.[j % 4]

let toOffset = function
    | North -> { X = 0; Y = 1 }
    | East -> { X = 1; Y = 0 }
    | South -> { X = 0; Y = -1 }
    | West -> { X = -1; Y = 0 }

let parse (inst: string) =
    let rot = match Seq.head inst with
              | 'L' -> Left
              | 'R' -> Right
              | c -> failwithf "invalid rotation '%c'" c
    let times = inst.Substring(1) |> Int32.Parse
    seq {
        yield Rotate rot
        yield! Move |> Seq.replicate (times - 1)
    }

let step (pos, dir) inst =
    match inst with
    | Rotate rot ->
        let dir' = rotate rot dir
        let offset = toOffset dir'
        let pos' = pos + offset
        (pos', dir')
    | Move ->
        let offset = toOffset dir
        let pos' = pos + offset
        (pos', dir)

let firstRepeat state pos =
    match state with
    | Collection set ->
        if Set.contains pos set then
            Result pos
        else
            Collection (Set.add pos set)
    | Result res -> Result res

let day1 (input: seq<string>) =
    let instructions = (input |> Seq.head).Split([|", "|], StringSplitOptions.RemoveEmptyEntries) |> Seq.collect parse
    let start = ({ X = 0; Y = 0 }, North)

    // Part 1
    instructions
    |> Seq.fold step start
    |> fst
    |> (fun pos -> pos.Distance)
    |> printfn "%i"

    // Part 2
    instructions
    |> Seq.scan step start
    |> Seq.map fst
    |> Seq.fold firstRepeat (Collection Set.empty)
    |> (function | Result pos -> pos.Distance | _ -> -1)
    |> printfn "%i"
    ()