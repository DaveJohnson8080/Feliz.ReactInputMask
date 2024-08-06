
open System
open System.Text.Json
open Fake.Core
open Fake.Core.TargetOperators
open Fake.Core.Xml
open Fake.DotNet
open Fake.IO
open Fake.IO.Globbing.Operators
open Fake.IO.FileSystemOperators
open Fake.JavaScript

let initializeContext () =
    let execContext = Context.FakeExecutionContext.Create false "build.fsx" []
    Context.setExecutionContext (Context.RuntimeContext.Fake execContext)

initializeContext()

let runOrDefault args =
    try
        match args with
        | [| target |] -> Target.runOrDefault target
        | _ -> Target.runOrDefault "Build"

        0
    with e ->
        printfn "%A" e
        1

let versionFromTag =
    match (Environment.environVarOrNone "APPVEYOR_REPO_TAG_NAME") with
    | Some s -> s
    | None -> "1.0.0"

let getExitCode (res: ProcessResult) =
  res.ExitCode

let failIfNonZero (exitCode: int) =
  if exitCode <> 0 then failwithf "Failed with exit code %i" exitCode

Target.create "Clean" (fun _ ->
  !! "**/bin"
  ++ "**/obj"
  ++ "**/dist"
  ++ "**/deploy"
  ++ "**/*.fable"
  -- "**/node_modules/**"
  -- "**/build/bin"
  |> Shell.cleanDirs
)

Target.create "DotNetRestore" (fun _ ->
  DotNet.exec id "paket" "restore" |> getExitCode |> failIfNonZero
)

Target.create "Build" (fun _ ->
  DotNet.build
    (fun c -> { c with Configuration = DotNet.BuildConfiguration.Release })
    "Feliz.ReactInputMask.sln"
)

Target.create "Pack" (fun _ ->
  DotNet.exec id "paket" (versionFromTag |> sprintf "pack --version %s dist") |> getExitCode |> failIfNonZero
)

Target.create "UpdatePackages" (fun _ ->
  DotNet.exec id "paket" "update" |> getExitCode |> failIfNonZero
  Npm.exec "up" (fun c -> { c with WorkingDirectory = "demo" } )
  DotNet.exec id "femto" "--resolve demo/src" |> getExitCode |> failIfNonZero
)

Target.create "UpdateFemtoVersionMetadata" (fun _ ->
  let npm = ProcessUtils.findFilesOnPath "npm" |> Seq.head
  let latestStableVersion =
    CreateProcess.fromRawCommand npm ["show"; "react-input-mask"; "versions"; "--json"]
    |> CreateProcess.withWorkingDirectory "demo"
    |> CreateProcess.redirectOutput
    |> Proc.run
    |> fun r -> r.Result.Output
    |> fun s -> printfn "%s" s; s
    |> JsonSerializer.Deserialize<seq<string>>
    |> Seq.map SemVer.parse
    |> Seq.filter (fun v -> v.PreRelease.IsSome)        // NOTE!!! : Allowing Pre-release since it's still beta.
    |> Seq.last

  poke
    "src/Feliz.ReactInputMask/Feliz.ReactInputMask.fsproj"
    "//NpmPackage[@Name='react-input-mask']/@Version"
    latestStableVersion.AsString
)

Target.create "CiBuild" ignore

let dependencies = [
  "Clean"
    ==> "DotNetRestore"
    ==> "Build"
    ==> "Pack"

  "Build"
    ==> "Pack"

  "Pack"
  ==> "CiBuild"
]

[<EntryPoint>]
let main args = runOrDefault args
