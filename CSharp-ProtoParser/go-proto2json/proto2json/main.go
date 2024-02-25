package main

import (
	"bytes"
	"encoding/json"
	"fmt"
	"io"
	"os"
	"path/filepath"
	"strings"

	"github.com/spf13/pflag"
	protoparser "github.com/yoheimuta/go-protoparser/v4"
	"github.com/yoheimuta/go-protoparser/v4/interpret/unordered"
)

const StdoutDefaultId = "__<>_stdout"

func parseProto(input io.Reader) (string, error) {
	result, err := protoparser.Parse(input)
	if err != nil {
		return "", err
	}

	unorderedResult, err := unordered.InterpretProto(result)
	if err != nil {
		return "", err
	}

	jsonBytes, err := json.Marshal(unorderedResult)
	if err != nil {
		return "", err
	}

	return string(jsonBytes), nil
}

func processFile(filePath string, outputDir string) error {
	reader, err := os.Open(filePath)
	if err != nil {
		return err
	}
	defer reader.Close()

	jsonResult, err := parseProto(reader)
	if err != nil {
		return err
	}

	if outputDir == StdoutDefaultId {
		fmt.Fprintf(os.Stdout, "%s=>%s\n", filePath, jsonResult)
	} else {
		outputPath := filepath.Join(outputDir, filepath.Base(filePath)+".json")
		err = os.WriteFile(outputPath, []byte(jsonResult), 0644)
		if err != nil {
			return err
		}
	}

	return nil
}

func processDir(dirPath string, outputDir string) error {
	err := filepath.Walk(dirPath, func(path string, info os.FileInfo, err error) error {
		if err != nil {
			return err
		}

		if !info.IsDir() && strings.HasSuffix(path, ".proto") {
			err := processFile(path, outputDir)
			if err != nil {
				return err
			}
		}

		return nil
	})

	return err
}

func main() {
	stdin := pflag.Bool("stdin", false, "Read from stdin")
	files := pflag.StringSlice("file", []string{}, "Read from specified file(s)")
	dir := pflag.String("dir", "", "Read from specified directory")
	fout := pflag.String("fout", StdoutDefaultId, "Output directory")

	pflag.Parse()

	outputDir := *fout

	if *stdin {
		stdindata, err := io.ReadAll(io.Reader(os.Stdin))
		if err != nil {
			fmt.Fprintf(os.Stderr, "Error reading stdin: %v\n", err)
			os.Exit(1)
		}
		// fmt.Fprintf(os.Stderr, "Read data from stdin")
		// last20Bytes := stdindata[len(stdindata)-20:]
		// for _, b := range last20Bytes {
		// 	fmt.Fprintln(os.Stderr, b)
		// }

		jsonResult, err := parseProto(bytes.NewReader(stdindata))
		if err != nil {
			fmt.Fprintf(os.Stderr, "Error parsing proto: %v\n", err)
			os.Exit(1)
		}

		if outputDir == StdoutDefaultId {
			fmt.Fprintf(os.Stdout, "%s", jsonResult)
		} else {
			outputPath := filepath.Join(outputDir, "parser.from-stdin.json")
			err = os.WriteFile(outputPath, []byte(jsonResult), 0644)
			if err != nil {
				fmt.Fprintf(os.Stderr, "Error writing output file: %v\n", err)
				os.Exit(1)
			}
		}

		return
	}

	for _, file := range *files {
		err := processFile(file, outputDir)
		if err != nil {
			fmt.Fprintf(os.Stderr, "Error processing file %s: %v\n", file, err)
			os.Exit(1)
		}
	}

	if *dir != "" {
		err := processDir(*dir, outputDir)
		if err != nil {
			fmt.Fprintf(os.Stderr, "Error processing directory %s: %v\n", *dir, err)
			os.Exit(1)
		}
	}
}
