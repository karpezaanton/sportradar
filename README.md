# Football Scoreboard

This repository contains a simple implementation of a Football Scoreboard service in C# using the Sportradar API for managing football matches.

## Table of Contents

- [Introduction](#introduction)
- [Components](#components)
- [Usage](#usage)

## Introduction

The Football Scoreboard project is designed to manage and track football matches, including starting new matches, updating scores, and finishing matches. It uses the Sportradar API for data retrieval and manipulation.

## Components

### Scoreboard

The `Scoreboard` class is responsible for managing football matches. It provides the following functionalities:

- `StartNewMatch`: Initiates a new football match between two teams.
- `UpdateScore`: Updates the score of an ongoing football match.
- `FinishMatch`: Marks a football match as finished.
- `GetMatches`: Retrieves a list of all football matches.

### FootballDataProvider

The `FootballDataProvider` class serves as the data source for the `Scoreboard` and is responsible for storing and managing football match data. It provides the following functionalities:

- `Matches`: Retrieves a list of all football matches.
- `StartMatch`: Starts a new football match.
- `UpdateMatch`: Updates the score of an ongoing football match.
- `FinishMatch`: Marks a football match as finished.

## Usage

To use the Football Scoreboard in your application:

1. Create an instance of `Scoreboard` and pass an instance of `FootballDataProvider` to its constructor.

2. Use the `Scoreboard` methods to manage football matches within your application.