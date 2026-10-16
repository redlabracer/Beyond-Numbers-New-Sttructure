# Translating Beyond Numbers

Translations are managed on **Crowdin**: <!-- TODO: replace with the public project link -->
`https://crowdin.com/project/<your-project>`

Anyone can join the project and translate in the browser. No GitHub account or coding is needed.

## Supported languages

The mod can only show languages that Cities: Skylines II itself ships:

| Crowdin language        | Game locale |
| ----------------------- | ----------- |
| English (source)        | `en-US`     |
| German                  | `de-DE`     |
| Spanish                 | `es-ES`     |
| French                  | `fr-FR`     |
| Italian                 | `it-IT`     |
| Japanese                | `ja-JP`     |
| Korean                  | `ko-KR`     |
| Polish                  | `pl-PL`     |
| Portuguese (Brazil)     | `pt-BR`     |
| Russian                 | `ru-RU`     |
| Chinese (Simplified)    | `zh-HANS`   |
| Chinese (Traditional)   | `zh-HANT`   |

Strings that are not translated yet fall back to English in-game.

## Notes for translators

- Translate only the text. Keep words like **City Watchdog** and **Universal Mod Menu** as they are; they are names of other mods.
- Keep the texts short; most of them are labels in the options screen or in a small in-game panel.
- `Options.OPTION:<Name>` is the label of a setting, `Options.OPTION_DESCRIPTION:<Name>` is its tooltip.

## Notes for developers

- `Beyond Numbers with STT/lang/en-US.json` is the **only** file you edit by hand. Every other file in `lang/` is written by Crowdin and arrives through the automated *New translations from Crowdin* pull request.
- All files in `lang/` are embedded into the DLL at build time and registered by `Locale/LocaleLoader.cs`.
- Settings keys are written in a short form and expanded to the game's real option keys when loading:

  | Key in JSON                                      | Meaning                          |
  | ------------------------------------------------ | -------------------------------- |
  | `Options.SECTION`                                | Mod name in the options menu     |
  | `Options.TAB:<tab>`                              | Options tab                      |
  | `Options.GROUP:<group>`                          | Options group header             |
  | `Options.OPTION:<Property>`                      | Setting label                    |
  | `Options.OPTION_DESCRIPTION:<Property>`          | Setting tooltip                  |
  | `Options.ENUM:<EnumType>.<Value>`                | Dropdown value                   |
  | anything else (e.g. `BeyondNumbers.*`)           | Used as-is (UI panel strings)    |

  A typo in an `Options.*:` key or a reference to a setting that does not exist is reported in the mod log instead of silently overwriting another string.
- Adding a new setting: add the property in `Setting.cs`, add its `Options.OPTION:` / `Options.OPTION_DESCRIPTION:` entries to `en-US.json`, push to `master`. The Crowdin workflow uploads the new strings automatically.
