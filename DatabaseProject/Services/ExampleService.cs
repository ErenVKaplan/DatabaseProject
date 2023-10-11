using DatabaseProject.Entities;

namespace DatabaseProject.Services
{
    public class ExampleService
    {
        private readonly AppDbContext _context;
        public ExampleService(AppDbContext context)
        {
            _context = context;
        }

        /*
        public IEnumerable<Example> GetByActive(bool isActive)
        {
			// tek bir eleman getirmek istendiğinde
            var _example = _context.Examples.Where(x => x.isActive == isActive).FirstOrDefault();
			// liste olarak getirilme istendiğinde
			var _examples = _context.Examples.Where(x => x.isActive == isActive).ToList();
			return _examples;
        }
		*/
    }
}
