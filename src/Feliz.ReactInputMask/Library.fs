namespace Feliz

open System.ComponentModel
open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open Feliz

[<EditorBrowsable(EditorBrowsableState.Never)>]
module Object =
    [<Emit("$0 === undefined")>]
    let private isUndefined x = jsNative

    let fromFlatEntries (kvs: seq<string * obj>) : obj =
        let rec setProperty (target : obj) (key : string) (value : obj) =
            match key.IndexOf '.' with
            | -1 -> target?(key) <- value
            | sepIdx ->
                let topKey = key.Substring (0, sepIdx)
                let nestedKey = key.Substring (sepIdx + 1)
                if isUndefined target?(topKey) then
                    target?(topKey) <- obj ()
                setProperty target?(topKey) nestedKey value

        let target = obj ()
        for (key, value) in kvs do
            setProperty target key value
        target

[<AutoOpen; EditorBrowsable(EditorBrowsableState.Never)>]
module ReactInputMaskHelpers =

    let reactElement (el: ReactElementType) (props: 'a) : ReactElement =
        import "createElement" "react"

    let reactElementTag (tag: string) (props: 'a) : ReactElement =
        import "createElement" "react"

    let createElement (el: ReactElementType) (properties: IReactProperty seq) : ReactElement =
        reactElement el (!!properties |> Object.fromFlatEntries)

    let createElementTag (tag: string) (properties: IReactProperty seq) : ReactElement =
        reactElementTag tag (!!properties |> Object.fromFlatEntries)

type inputMask =
    static member inline mask (value : string) = Interop.mkAttr "mask" value
    static member inline maskChar (value : string) = Interop.mkAttr "maskChar" value
    static member inline formatChars (value : Map<string, string>) = Interop.mkAttr "formatChars" value
    static member inline alwaysShowMask (value : bool) = Interop.mkAttr "alwaysShowMask" value
    static member inline children (element: unit -> ReactElement) = Interop.mkAttr "children" element

type InputMask =
    static member inline inputMask props = createElement (importDefault "react-input-mask") props