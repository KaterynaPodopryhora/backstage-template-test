const fs = require('fs')
const util = require('util')

const nunjucks = require('nunjucks')
const glob = require('glob')
const ncp = require('ncp').ncp
const path = require('path')

const customNunjucks = new nunjucks.Environment(new nunjucks.FileSystemLoader(),
                          { 
                            autoescape: false,
                            tags : {
                              variableStart: '${{',
                              variableEnd: '}}'
                            }
                          });

ncp.limit = 16

const DIST_FOLDER = './dist'

async function run() {
  // Get configuration
  const folder = process.argv[2];
  if (!folder) {
    throw "add commandline argument for folder"
  }

  const templateValuesFilePath = process.argv[3];
  if (!templateValuesFilePath) {
    throw "add commandline argument for templateValueFilePath"
  }
  const templateValues = getTemplateValues(templateValuesFilePath);
  console.log(templateValues);

  // Prepare
  createEmptyDistFolder()
  await copyFilesToDist(folder)

  // apply templating logic
  applyTemplate(folder, templateValues)
  renameFolders(folder, templateValues)
  renameFiles(folder, templateValues)
}

function getTemplateValues(path) {
  const content = fs.readFileSync(path, {encoding: 'utf-8'});
  return JSON.parse(content);
}

function renameFolders(folder, templateValues) {
  const splitChar = '/'
  const items = glob.sync(`./dist/${folder}/**`, {});
  let dirs = items.filter((file) => fs.lstatSync(file).isDirectory())

  // sort to prevent changing a shallow path first
  dirs = dirs.sort((a,b) => a.split(splitChar).length > b.split(splitChar).length);

  dirs.forEach((dirPath) => {
    // some logic to only rename the last directory in the path
    var pathParts = dirPath.split('/')
    var lastDir = pathParts[pathParts.length-1]
    var lastDirTemplated = customNunjucks.renderString(lastDir, templateValues)
    if (lastDir === lastDirTemplated) {
      return //no need to rename
    }
    pathParts.pop()
    pathParts.push(lastDirTemplated);
    // create new path from array
    let newDirPath = pathParts[0];
    for (let i = 1; i < pathParts.length; i++) {
      newDirPath = path.join(newDirPath, pathParts[i]);
    }

    fs.renameSync(dirPath, newDirPath)
  })
}

function renameFiles(folder, templateValues) {
  const items = glob.sync (`./dist/${folder}/**/**.*`, {});
  const files = items.filter((file) => fs.lstatSync(file).isFile())

  // order
  files.forEach((path) => renameFile(path, templateValues))
}

function renameFile(filePath, templateValues) {
  const newPath = customNunjucks.renderString(filePath, templateValues);
  fs.renameSync(filePath, newPath)
}

function copyFilesToDist(folder) {
  const ncpPromise = util.promisify(ncp)
  return ncpPromise(`./${folder}`, `./dist/${folder}`)
}

function createEmptyDistFolder() {
  if (fs.existsSync(DIST_FOLDER)) {
    fs.rmdirSync(DIST_FOLDER, { recursive: true })
  }
  fs.mkdirSync(DIST_FOLDER)
}

function applyTemplate(folder, templateConfig) {
  const files = glob.sync(`./dist/${folder}/**/**.*`)
  files.forEach((file) => doTemplate(file, templateConfig))
}

function doTemplate(file, templateConfig) {
  if (fs.lstatSync(file).isDirectory()) {
    return
  }

  var output = customNunjucks.render(file, templateConfig)
  fs.writeFileSync(file, output)
}

run().catch((err) => {
  console.error(err)
  process.exit(1)
})