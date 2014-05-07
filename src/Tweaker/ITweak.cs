using CsQuery;

namespace Tweaker
{
    interface ITweak
    {
        void Run(CQ doc);
    }
}