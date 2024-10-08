# Unity Subtitle System
![Unity Version](https://img.shields.io/badge/unity-2020.3%2B-blue.svg) ![License](https://img.shields.io/badge/license-MIT-green.svg)

## Overview

**Unity Subtitle System** is an easy-to-use script for managing subtitles in Unity games. It allows you to load subtitles from a custom text file format and display them with custom durations. The script also includes a custom editor tool for uploading subtitle files directly from the Unity Inspector.

## Features

- Load subtitles from `.txt` files with a custom format.
- Display subtitles with specified durations.
- Toggle subtitles on/off in the game.
- Includes a custom editor for uploading subtitle files in the Inspector.

## Table of Contents

- [Installation](#installation)
- [How to Use](#how-to-use)
- [File Format](#file-format)
- [Screenshots](#screenshots)
- [Contributing](#contributing)
- [License](#license)

## Installation

1. Clone or download this repository.
2. Import the `SubtitleManager.cs` and `SubtitleManagerEditor.cs` scripts into your Unity project.
3. Create a UI Text object using TextMeshPro, and assign it to the `SubtitleManager` component.

```bash
git clone https://github.com/LordEvilM44Dev/Unity-Subtitle-System.git
```

## How To Use
1. Create a .txt file with the following format:

```
[DURATION="00:10"]
[SUBTITLE="Hello, welcome to our game!"]
```

2. In the Unity Inspector, click on the "Upload Subtitles (.txt)" button and select your subtitle file.
3. Set showSubtitles to true in the Inspector to display subtitles during gameplay.

## File Format
Each subtitle entry in the file should have this format:
```
[DURATION="MM:SS"]
[SUBTITLE="Your subtitle text here"]
```

## Screenshots

## Contributing
Contributions are welcome! Please follow the guidelines below:
1. Fork the project.
2. Create a new branch ```(git checkout -b feature/your-feature)```.
3. Commit your changes ```(git commit -m 'Add some feature')```.
4. Push to the branch ```(git push origin feature/your-feature)```.
5. Open a pull request.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
