function init() {}

function log(error : any) {
  console.error(error);
}

const logger = {
  init,
  log
};

export default logger;
