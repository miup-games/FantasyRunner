#!/bin/bash
GENERATED_CODE_FOLDER="../Assets/FantasyRunner/Data/GeneratedCode"
GENERATED_DATA_FOLDER="../Assets/FantasyRunner/Data/Resources"

echo "----------------------"
echo "Generating Interfaces"
java -jar transformo.jar -d FantasyRunner-Database.xlsx  -fields -sfolder Templates/CSharp-Interfaces -tfolder $GENERATED_CODE_FOLDER/Interfaces/Fields -tfile 'I$field_type:class_case$$field_name:class_case$.cs'

echo "----------------------"
echo "Generating Models Interfaces"
java -jar transformo.jar -d FantasyRunner-Database.xlsx -tables -sfolder Templates/CSharp-ModelInterfaces -tfolder $GENERATED_CODE_FOLDER/Interfaces/Models -tfile 'I$table_name:class_case$.cs'

echo "----------------------"
echo "Generating Models"
java -jar transformo.jar -d FantasyRunner-Database.xlsx -tables -sfolder Templates/CSharp-Models -tfolder $GENERATED_CODE_FOLDER/Models -tfile '$table_name:class_case$.cs'

echo "----------------------"
echo "Generating View Models"
java -jar transformo.jar -d FantasyRunner-Database.xlsx -tables -sfolder Templates/CSharp-ViewModels -tfolder $GENERATED_CODE_FOLDER/ViewModels -tfile '$table_name:class_case$ViewModel.cs'

echo "----------------------"
echo "Generating Data"
java -jar transformo.jar -d FantasyRunner-Database.xlsx -data -json -tfile $GENERATED_DATA_FOLDER/database.json
