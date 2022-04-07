<p align="center"><img src="https://capsule-render.vercel.app/api?type=soft&fontColor=FFA73E&text=Charles-Ay/NovelReader&height=170&fontSize=60&desc=It%20Works%20:P&descAlignY=75&descAlign=15&color=00000000&animation=twinkling"></p>


<p align="center"><a href="https://github.com/Charles-Ay/NovelReader"><img src="https://forthebadge.com/images/badges/0-percent-optimized.svg" height="30px"><img src="https://forthebadge.com/images/badges/made-with-c-sharp.svg" height="30px"></a></p>
</h1>

## Installation

To use NovelReader. Download the [**NovelReader.exe**](https://github.com/Charles-Ay/NovelReader/tree/main/MainProgram). You can also clone the [**Project**](https://github.com/Charles-Ay/NovelReader) -> build -> create a single exe using the [**ILMergeGUI.exe**](https://github.com/Charles-Ay/NovelReader/tree/main/ILMergeGUILatest) and add the build .exe file and .dlls

**Support:** Python 3.6 and higher

### Installing [`mpv`](https://github.com/mpv-player/mpv/)

#### Windows:

Only Supports Windows for now.

#### Linux:

Planned for the future or if anyone else wants to create a linux version.

#### Mac:

Gross. JKJK. I'm too poor to affored one so can't make a compatable version :(

### Core features

- Took longer than I thought it would
- Scrapes the Sources for novel links and request to raw HTML, So its efficient ig?? 🤷
- Doesn't use any heavy dependencies such as Selenium or Javascript Evaluators.
- Mostly uses C# standared libs so pretty lightweight.
- Integrates HtmlAgilityPack for efficent and effective parsing.

### Usage

```
Usage: NovelReader [OPTIONS] [ARGS]

Options:
  -h  Show this message and exit.
  -d  Use Dev mode.
```

**Examples:**

Download **Overgeared**'s chapter 1-10 using [**FreeWebNovel.com**](https://FreeWebNovel.com/):

- ```
  NovelReader Overgeared 1-10
  ```
OR
- ```
  NovelReader Overgeared 1-10 freewebnovel
  ```

Download **Peerless Martial God**'s chapter 8 using [**NoveTrench.com**](https://noveltrench.com/) in dev mode:

- ```
  NovelReader -d "Peerless Martial God" 8 noveltrench
  ```

### Supported Sites

| Website                                      | Source Prefix       |
| :------------------------------------------: | :-----------------: |
| [FreeWebNovel.com](https://FreeWebNovel.com/)| `allanime`          |
| [NoveTrench.com](https://noveltrench.com/)   |`noveltrench`        |

### More sites?

Yeah, ill add some more when I get around to it. You can raise a issue if you really want a specific one. Please try to make sure that the websites you request meet mostof the following criteria(its a headache to work with sites that dont and I don't want to add a webdriver as that add lots of overhead)
- Supports HTTP Request and doesnt hide the content inside obscure Java-Script **Cough, Cough** WuxiaWorld
- Should have some sort of search function(like a search bar)
- The chapters should have some logical ordering/naming. Not something like https://trashnovelsite.com/novel/32113/chapter-3. This makes it so much harder to get the right novel

**Note:** Your request may be denied in case of Cloudflare protections and powerful anti-bot scripts(ew).

### Disclaimer

The disclaimer of this project can be found [here.](./DISCLIAMER.md)(usual legal mobo-jumbo)