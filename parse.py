import json
import requests
import os

from typing import List

dump_data = False


def parse(content: str, separator: str) -> list:
    l = []
    rows = content.splitlines()
    headers = rows[0].split(separator)
    for row in rows[1:]:
        cols = row.split(',')
        l.append({k: v for (k, v) in zip(headers, cols)})
    return l


def load_data(region: str, name: str, lines: List[str], dataset: dict):
    data_line: str = next(
        filter(lambda row: row.startswith('data-'), lines), None)
    if not data_line:
        print(f'Fail found data in dataset: {name}')
        return

    data_url = next(filter(lambda col: 'http' in col,
                           data_line.split(',')), None)

    if not data_url:
        print(f'Fail fount url in data line: {data_url}')
        return

    try:
        resp = requests.get(data_url)
    except:
        print(f'Fail load data from <{data_url}>')
        return

    if resp.status_code != 200:
        print(f'Fail load data: {resp.status_code}')
        return

    content: str = resp.content.decode("utf-8")
    dataset['data'] = parse(content, ';')

    if dump_data:
        with open(f'./tmp/{region}/{name}_data.csv', 'w', encoding='utf-8') as f:
            f.write(content)


def load_structure(region: str, name: str, lines: List[str], dataset: dict):
    structure_line: str = next(
        filter(lambda row: row.startswith('structure-'), lines), None)

    if not structure_line:
        print(f'Fail found structure in dataset: {name}')
        return

    structure_url = next(filter(lambda col: 'http' in col,
                                structure_line.split(',')), None)

    if not structure_url:
        print(f'Fail fount url in structure line: {structure_url}')
        return

    try:
        resp = requests.get(structure_url)
    except:
        print(f'Fail load structure from <{structure_url}>')
        return

    if resp.status_code != 200:
        print(f'Fail load structure: {resp.status_code}')
        return

    content: str = resp.content.decode("utf-8")
    dataset['structure'] = parse(content, ',')

    if dump_data:
        with open(f'./tmp/{region}/{name}_structure.csv', 'w', encoding='utf-8') as f:
            f.write(content)


def load_dataset(region: str, name: str, content: str, db: dict):
    dataset: dict = {}
    dataset['meta'] = parse(content, ',')

    lines: List[str] = content.splitlines()

    load_data(region, name, lines, dataset)
    load_structure(region, name, lines, dataset)

    db[name] = dataset


def load_dataset_meta(region: str, line: str, db: dict):
    cols: List[str] = line.split(',')
    name: str = cols[0]
    url = next(filter(lambda col: 'http' in col, cols), None)

    if not url:
        print(f'Line does not contain url: {line}')
        return

    try:
        resp = requests.get(url)
    except:
        print(f'Fail load dataset from <{url}>')
        return

    if resp.status_code != 200:
        print(f'Fail load data set: {resp.status_code}')
        return

    content: str = resp.content.decode("utf-8")
    load_dataset(region, name, content, db)

    if dump_data:
        with open(f'./tmp/{region}/{name}_meta.csv', 'w', encoding='utf-8') as f:
            f.write(content)


def load_datasets(region: str, list_url: str, db: dict):
    resp = requests.get(list_url)
    if resp.status_code != 200:
        return

    os.makedirs(f'./tmp/{region}', exist_ok=True)

    content: str = resp.content.decode("utf-8")
    lines = content.splitlines()
    region_db: dict = {}
    for line in lines[1:]:
        load_dataset_meta(region, line, region_db)

    db[region] = region_db

    if dump_data:
        with open(f'./tmp/{region}/list.csv', 'w', encoding='utf-8') as f:
            f.write(content)


database: dict = {}

load_datasets('34', 'http://opendata.volganet.ru/list.csv', database)

with open('./tmp/data.db', 'w', encoding='utf-8') as f:
    f.write(json.dumps(database, indent=4, ensure_ascii=False))
